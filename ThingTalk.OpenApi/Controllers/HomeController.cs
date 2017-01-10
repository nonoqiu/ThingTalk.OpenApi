using System.Web.Mvc;
using ThingTalk.OpenApi.Application.Interfaces;

namespace ThingTalk.OpenApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        // 测试依赖注入
        //private IRefreshTokenService _refreshTokenService;
        ///// <summary>
        ///// 构造函数：传入IRefreshTokenService接口实例
        ///// </summary>
        ///// <param name="refreshTokenService"></param>
        //public HomeController(IRefreshTokenService refreshTokenService)
        //{
        //    _refreshTokenService = refreshTokenService;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // _refreshTokenService.Get("sss");
            return View();
        }
    }
}
