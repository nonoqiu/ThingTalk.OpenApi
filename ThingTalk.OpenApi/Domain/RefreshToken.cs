using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;

namespace ThingTalk.OpenApi.Application.Domain
{
    /// <summary>
    /// 用户Token实体
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// refreshToken值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }        
        /// <summary>
        /// 客户ID
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime IssuedUtc { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiresUtc { get; set; }
        /// <summary>
        /// Ticket凭据
        /// </summary>
        public string ProtectedTicket { get; set; }
    }
}