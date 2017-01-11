using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Infrastructure;
using System.Collections.Concurrent;
using System;
using ThingTalk.OpenApi.Application.Interfaces;
using System.Security.Cryptography;
using ThingTalk.OpenApi.Application.Domain;

namespace ThingTalk.OpenApi.Providers
{
    /// <summary>
    /// 信云客户端权限刷新验证持久化提供者
    /// </summary>
    public class ThingTalkPersistenceRefreshTokenProvider : AuthenticationTokenProvider
    {
        private IRefreshTokenService _refreshTokenService;
        /// <summary>
        /// 构造函数之信云客户端权限刷新验证持久化提供者
        /// </summary>
        /// <param name="refreshTokenService"></param>
        public ThingTalkPersistenceRefreshTokenProvider(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }
        /// <summary>
        /// 用RNGCryptoServiceProvider生成refresh token，并获取相关信息，创建RefreshToken
        /// 并调用 IRefreshTokenService.Save() 进行持久化保存。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientID = context.OwinContext.Get<string>("as:client_id");
            if (string.IsNullOrEmpty(clientID)) return;

            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");
            if (string.IsNullOrEmpty(refreshTokenLifeTime)) return;

            // 生成access token
            RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[50];
            cryptoRandomDataGenerator.GetBytes(buffer);
            var refreshTokenId = Convert.ToBase64String(buffer).TrimEnd('=').Replace('+', '-').Replace('/', '_');

            var refreshToken = new RefreshToken()
            {
                Id = refreshTokenId,
                ClientId = clientID,
                UserName = context.Ticket.Identity.Name,
                Password = "",
                //Password = (context.Ticket.Identity as ClaimsIdentity).FindFirst("userdata").Value,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddSeconds(Convert.ToDouble(refreshTokenLifeTime)),
                ProtectedTicket = context.SerializeTicket()
            };
            context.Ticket.Properties.IssuedUtc = refreshToken.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = refreshToken.ExpiresUtc;

            if (await _refreshTokenService.Save(refreshToken))
            {
                context.SetToken(refreshTokenId);
            }
        }
        /// <summary>
        /// 调用 IRefreshTokenService.Get() 获取 RefreshToken，用它反序列出生成access token所需的ticket，
        /// 从持久化中删除旧的refresh token（刷新access token时，refresh token也会重新生成）。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var refreshToken = await _refreshTokenService.Get(context.Token);
            if (refreshToken != null)
            {
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                var result = await _refreshTokenService.Remove(context.Token);
            }
        }
    }
}