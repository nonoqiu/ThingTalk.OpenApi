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
        /// <returns>操作结果</returns>
        [HttpGet]
        public BaseResult Logout()
        {
            var oAuthIdentity = User.Identity as ClaimsIdentity;
            var entity = _refreshTokenService.FindByUserName(oAuthIdentity.Name);
            if (entity != null)
            {
                _refreshTokenService.Remove(entity.Id);
            }

            var baseRet = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_SUCCESS, "Logout");
            return baseRet;
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="OldPassword"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<BaseResult> ChangePassword(string OldPassword, string NewPassword)
        {
            var type = "ChangePassword";
            var oAuthIdentity = User.Identity as ClaimsIdentity;
            var entity = _refreshTokenService.FindByUserName(oAuthIdentity.Name);
            if (entity == null)
            {
                var baseResult1 = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_NoAuthorized, type);
                return baseResult1;
            }

            var authData = oAuthIdentity.Name.Split(',');
            if (authData.Length > 2)
            {
                var username = authData[0];
                var deptcode = authData[1];
                var baseResult2 = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_SUCCESS, type);
                if (!await _clientService.ChangePassword(authData[0], authData[1], OldPassword, NewPassword))
                {
                    baseResult2 = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_NoAuthorized, type);
                }
                return baseResult2;
            }

            return BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_NoAuthorized, type);
        }
    }
}