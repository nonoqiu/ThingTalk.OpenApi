using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;

namespace ThingTalk.OpenApi.Application.Domain
{
    /// <summary>
    /// 帐户信息
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 企业码
        /// </summary>
        public string DeptCode { get; set; }
    }
}