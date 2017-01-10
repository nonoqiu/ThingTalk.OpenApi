using System.Collections.Generic;
using System.Web.Http;
using ThingTalk.OpenApi.Models;
using ThingTalk.OpenApi.Providers;
using Newtonsoft.Json;
using System.Security.Claims;
using ThingTalk.OpenApi.Application.Interfaces;

namespace ThingTalk.OpenApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class ValuesController : ApiController
    {
        //private IRefreshTokenRepository repository;

        private IRefreshTokenService _refreshTokenService;
        /// <summary>
        /// 构造函数：传入IRefreshTokenService接口实例
        /// </summary>
        /// <param name="refreshTokenService"></param>
        public ValuesController(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        /// <summary>
        /// GET api/values
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            // User
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// GET api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="value"></param>
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// PUT api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// DELETE api/values/5
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName">提交的参数</param>
        /// <returns>用户信息</returns>
        [HttpGet]
        public string GetUser(string userName)
        {
            _refreshTokenService.Get("sss");

            var oAuthIdentity = User.Identity as ClaimsIdentity;
            //var identity = string.Format("AuthenticationType:{0},IsAuthenticated:{1},Name:{2}-{3},UserData:{4}.",
            //    User.Identity.AuthenticationType, User.Identity.IsAuthenticated, User.Identity.Name, oAuthIdentity.Name, oAuthIdentity.FindFirst("userdata").Value);
            var identity = string.Format("AuthenticationType:{0},IsAuthenticated:{1},Name:{2}.",
                User.Identity.AuthenticationType, User.Identity.IsAuthenticated, User.Identity.Name);

            var refreshToken = _refreshTokenService.Get(User.Identity.Name);
            identity += ", " + refreshToken.Id;

            var json = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_NoAuthorized);
            json.Type = "Login:" + identity;
            if (string.IsNullOrEmpty(userName))
            {
                return JsonConvert.SerializeObject(json);
            }

            if (userName.Equals("trutalk"))
            {
                var entity = new TruTalkResult<UserInfo>
                {
                    BaseResult = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_SUCCESS),
                    Data = new UserInfo()
                    {
                        DeptCode = "270806",
                        Password = "888888",
                        UserName = userName
                    }
                };
                entity.BaseResult.Type = "GetUser";
                return JsonConvert.SerializeObject(entity);
            }

            json = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_UserUnLogin);
            json.Type = "Login:" + identity;
            return JsonConvert.SerializeObject(json);
        }
    }
}