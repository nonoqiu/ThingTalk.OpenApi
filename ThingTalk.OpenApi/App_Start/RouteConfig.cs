using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThingTalk.OpenApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //// 自定义路由映射
            //var defaults = new RouteValueDictionary { { "name", "*" }, { "id", "*" } };
            //var _name = "TruTalkService";
            //var _url = "~/TruTalkService.asmx";
            //routes.MapPageRoute(_name, "TruTalkService", _url);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}