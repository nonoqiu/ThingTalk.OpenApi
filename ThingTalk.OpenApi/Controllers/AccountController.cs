using System.Collections.Generic;
using System.Web.Http;
using ThingTalk.OpenApi.Models;
using ThingTalk.OpenApi.Providers;
using Newtonsoft.Json;
using System.Security.Claims;
using ThingTalk.OpenApi.Application.Interfaces;
using ThingTalk.OpenApi.Application.Services;
using ThingTalk.OpenApi.Repository.FileStorage;
using System.Threading.Tasks;

namespace ThingTalk.OpenApi.Controllers
{
    /// <summary>
    /// 帐号相关接口
    /// </summary>
    [Authorize]
    public class AccountController : ApiController
    {
        private IClientService _clientService;
        private IRefreshTokenService _refreshTokenService;

        /// <summary>
        /// 构造函数：传入IRefreshTokenService接口实例
        /// </summary>
        /// <param name="refreshTokenService"></param>
        public AccountController(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
            _clientService = new ClientService(new ClientRepository());
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Logout()
        {
            var oAuthIdentity = User.Identity as ClaimsIdentity;
            var entity = _refreshTokenService.FindByUserName(oAuthIdentity.Name);
            if (entity != null)
            {
                _refreshTokenService.Remove(entity.Id);
            }

            var json = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_SUCCESS, "Logout");
            return JsonConvert.SerializeObject(json);
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="OldPassword"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        [HttpGet]        
        public async Task<string> ChangePassword(string OldPassword, string NewPassword)
        {
            var type = "ChangePassword";
            var oAuthIdentity = User.Identity as ClaimsIdentity;
            var entity = _refreshTokenService.FindByUserName(oAuthIdentity.Name);
            if (entity == null)
            {
                var json = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_NoAuthorized, type);
                json.Type = type;
                return JsonConvert.SerializeObject(json);
            }

            var authData = oAuthIdentity.Name.Split(',');
            if (authData.Length > 2)
            {
                var username = authData[0];
                var deptcode = authData[1];
                var json = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_SUCCESS, type);
                if (await _clientService.ChangePassword(authData[0], authData[1], OldPassword, NewPassword))
                {
                    json.Type = type;
                }
                else
                {
                    json = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_NoAuthorized, type);
                    json.Type = type;
                }
                return JsonConvert.SerializeObject(json);
            }

            var _respjson = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_NoAuthorized, type);
            _respjson.Type = type;
            return JsonConvert.SerializeObject(_respjson);
        }
    }
}