using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using ThingTalk.OpenApi.Application.Interfaces;
using System.Security.Cryptography;
using System;

namespace ThingTalk.OpenApi.Providers
{
    /// <summary>
    /// 信云客户端权限验证提供者
    /// </summary>
    public class ThingTalkApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private IClientService _clientService;
        /// <summary>
        /// 构造函数之客户端权限验证提供者
        /// </summary>
        /// <param name="clientService"></param>
        public ThingTalkApplicationOAuthProvider(IClientService clientService)
        {
            _clientService = clientService;
        }
        /// <summary>
        /// 获取客户端的 client_id 与 client_secret 进行验证
        /// client_id:      用户名,企业码
        /// client_secret:  登录密码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            context.TryGetBasicCredentials(out clientId, out clientSecret);

            var client = await _clientService.Get(clientId);
            if (client == null) { return; }
            if (client.Secret != clientSecret) { return; }

            context.OwinContext.Set<string>("as:client_id", clientId);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());
            context.Validated(clientId);

            await base.ValidateClientAuthentication(context);
        }
        /// <summary>
        /// 对客户端进行授权，授了权就能发 access token, 适用 grant_type: client_credentials
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "ThingTalk App"));
            //context.Validated(oAuthIdentity);

            var props = new AuthenticationProperties(new Dictionary<string, string> { { "as:client_id", context.ClientId } });
            var ticket = new AuthenticationTicket(oAuthIdentity, props);
            context.Validated(ticket);

            return base.GrantClientCredentials(context);
        }
        /// <summary>
        /// 调用后台的登录服务验证用户名与密码, 适用 grant_type: password        
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);

            // 可以缓存客户的身份信息
            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.UserData, context.Password));

            // 身份验证
            // 生成唯一的客户信息
            RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[50];
            cryptoRandomDataGenerator.GetBytes(buffer);
            var userName = Convert.ToBase64String(buffer).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName + "," + userName));
            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.UserData, userName));

            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(ticket);

            await base.GrantResourceOwnerCredentials(context);
        }
        /// <summary>
        /// 验证持有 refresh token 的客户端
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.Rejected();
                return;
            }

            var newId = new ClaimsIdentity(context.Ticket.Identity);
            newId.AddClaim(new Claim("newClaim", "refreshToken"));

            var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);
            context.Validated(newTicket);

            await base.GrantRefreshToken(context);
        }
    }
}