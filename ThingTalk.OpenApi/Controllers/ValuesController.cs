using System.Collections.Generic;
using System.Web.Http;
using ThingTalk.OpenApi.Models;
using ThingTalk.OpenApi.Providers;
using Newtonsoft.Json;

namespace ThingTalk.OpenApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class ValuesController : ApiController
    {
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
            var identity = string.Format("AuthenticationType:{0},IsAuthenticated:{1},Name:{2}.", User.Identity.AuthenticationType, User.Identity.IsAuthenticated, User.Identity.Name);

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
                return JsonConvert.SerializeObject(entity);
            }

            json = BaseResultHelper.ReturnBaseResult(E_TRUTALK_RESP.RESP_UserUnLogin);
            json.Type = "Login:" + identity;
            return JsonConvert.SerializeObject(json);
        }
    }
}