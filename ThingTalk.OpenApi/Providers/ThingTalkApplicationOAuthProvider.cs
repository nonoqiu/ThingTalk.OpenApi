using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace ThingTalk.OpenApi.Providers
{
    /// <summary>
    /// 信云客户端权限验证提供者
    /// </summary>
    public class ThingTalkApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// 获取客户端的 client_id 与 client_secret 进行验证
        /// client_id:      用户名,企业码
        /// client_secret:  登录密码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            context.TryGetBasicCredentials(out clientId, out clientSecret);

            if (clientId == "1234" && clientSecret == "5678")
            {
                context.Validated(clientId);
            }
            return base.ValidateClientAuthentication(context);
        }
        /// <summary>
        /// 对客户端进行授权，授了权就能发 access token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "IOS App"));

            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(ticket);

            return base.GrantClientCredentials(context);
        }
        /// <summary>
        /// 调用后台的登录服务验证用户名与密码(此处不验证)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.UserData, context.Password));
            //var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            //context.Validated(ticket);

            await base.GrantResourceOwnerCredentials(context);
        }
    }
}