using System.Web.Mvc;
using ThingTalk.OpenApi.Application.Interfaces;

namespace ThingTalk.OpenApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class TruTalkServiceController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public string RequestPhoneData(string requestCmd, string requestData)
        public string RequestPhoneData(string name, string id)
        {
            return "hello";
        }
    }
}