using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using ThingTalk.OpenApi.Models;
using System.Configuration;

namespace ThingTalk.OpenApi.Providers
{
    /// <summary>
    /// 回复信息帮助
    /// </summary>
    public class BaseResultHelper
    {
        private static Dictionary<E_TRUTALK_RESP, BaseResult> _dicRespDesc = new Dictionary<E_TRUTALK_RESP, BaseResult>();
        /// <summary>
        /// 创建回复信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static BaseResult ReturnBaseResult(E_TRUTALK_RESP type)
        {
            if (_dicRespDesc.Count == 0)
            {
                _dicRespDesc.Add(E_TRUTALK_RESP.RESP_NoAuthorized, new BaseResult { Code = "1001", Message = "未授权!" });
                _dicRespDesc.Add(E_TRUTALK_RESP.RESP_TimeOut, new BaseResult { Code = "1002", Message = "已超时!" });
                _dicRespDesc.Add(E_TRUTALK_RESP.RESP_TokenExpire, new BaseResult { Code = "1003", Message = "Access_Token已过期!" });
                _dicRespDesc.Add(E_TRUTALK_RESP.RESP_TokenInvalid, new BaseResult { Code = "1004", Message = "Access_Token无效!" });
                _dicRespDesc.Add(E_TRUTALK_RESP.RESP_UserUnLogin, new BaseResult { Code = "2001", Message = "用户未登录!" });
                _dicRespDesc.Add(E_TRUTALK_RESP.RESP_UserMsgError, new BaseResult { Code = "2002", Message = "用户信息错误!" });
                _dicRespDesc.Add(E_TRUTALK_RESP.RESP_UserUnExist, new BaseResult { Code = "2003", Message = "用户不存在!" });
                _dicRespDesc.Add(E_TRUTALK_RESP.RESP_ServiceRelate, new BaseResult { Code = "3001", Message = "业务相关..." });
                _dicRespDesc.Add(E_TRUTALK_RESP.RESP_SYSError, new BaseResult { Code = "5001", Message = "系统异常!" });
                _dicRespDesc.Add(E_TRUTALK_RESP.RESP_SUCCESS, new BaseResult { Code = "8888", Message = "操作成功!" });
            }
            return _dicRespDesc[type];
        }
    }
}