using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThingTalk.OpenApi.Models
{
    /// <summary>
    /// 回复枚举类
    /// 1XXX: 授权相关
    /// 2XXX: 用户相关
    /// 3XXX: 业务相关
    /// 5XXX: 系统相关
    /// </summary>
    public enum E_TRUTALK_RESP : ushort
    {
        /// <summary>
        /// 无权限访问
        /// </summary>
        RESP_NoAuthorized = 1001,
        /// <summary>
        /// 超时
        /// </summary>
        RESP_TimeOut = 1002,
        /// <summary>
        /// access_token过期
        /// </summary>
        RESP_TokenExpire = 1003,
        /// <summary>
        /// unique_token无效
        /// </summary>
        RESP_TokenInvalid = 1004,

        /// <summary>
        /// 用户未登录
        /// </summary>
        RESP_UserUnLogin = 2001,
        /// <summary>
        /// 用户信息错误
        /// </summary>
        RESP_UserMsgError = 2002,
        /// <summary>
        /// 用户不存在
        /// </summary>
        RESP_UserUnExist = 2003,

        /// <summary>
        /// 业务相关
        /// </summary>
        RESP_ServiceRelate = 3001,

        /// <summary>
        /// 系统异常
        /// </summary>
        RESP_SYSError = 5001,

        /// <summary>
        /// 操作成功
        /// </summary>
        RESP_SUCCESS = 8888
    }
}