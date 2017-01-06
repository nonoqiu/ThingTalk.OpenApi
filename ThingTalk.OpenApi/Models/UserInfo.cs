using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThingTalk.OpenApi.Models
{
    /// <summary>
    /// 用户信息实体
    /// </summary>
    public class UserInfo
    {
        public string DeptCode { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}