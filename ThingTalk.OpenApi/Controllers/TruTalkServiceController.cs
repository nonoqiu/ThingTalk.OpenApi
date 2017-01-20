using System;
using System.Web.Http;

namespace ThingTalk.OpenApi.Controllers
{
    /// <summary>
    /// WebService请求控制器
    /// </summary>
    public class TruTalkServiceController : ApiController
    {
        /// <summary>
        /// 兼容之前的WebService请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/ThingTalkServices/TruTalkService.asmx/RequestPhoneData")]
        public string RequestPhoneData(string requestCmd = "", string requestData = "")
        {
            var requestUrl = this.Request.RequestUri.ToString().Replace("queryRequest", "requestData");
            if (string.IsNullOrEmpty(requestData)) requestData = requestUrl.Substring(requestUrl.IndexOf("requestData") + "requestData".Length + 1);

            Console.WriteLine("RequestUri: " + this.Request.RequestUri);

            return "HELLO: " + requestCmd + ", " + requestData;
        }
    }
}