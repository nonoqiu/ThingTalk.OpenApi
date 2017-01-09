using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Infrastructure;
using System.Collections.Concurrent;
using System;

namespace ThingTalk.OpenApi.Providers
{
    /// <summary>
    /// 信云客户端权限刷新验证提供者
    /// </summary>
    public class ThingTalkRefreshTokenProvider : AuthenticationTokenProvider
    {
        private static ConcurrentDictionary<string, string> _refreshTokens = new ConcurrentDictionary<string, string>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Create(AuthenticationTokenCreateContext context)
        {
            string tokenValue = Guid.NewGuid().ToString("n");
            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(60);

            _refreshTokens[tokenValue] = context.SerializeTicket();
            context.SetToken(tokenValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_refreshTokens.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }
    }
}