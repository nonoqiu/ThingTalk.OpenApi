using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ThingTalk.OpenApi.App_Start;
using ThingTalk.OpenApi.Models;
using ThingTalk.OpenApi.Providers;

namespace ThingTalk.OpenApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IocConfig.RegisterDependencies();

            AppDomain.CurrentDomain.FirstChanceException += new EventHandler<FirstChanceExceptionEventArgs>((source, e) =>
            {
                MyLog4NetInfo.LogInfo(string.Format("统一异常记录，{0}", e.Exception));
                MyLog4NetInfo.LogInfo(string.Format("统一异常记录，{0}", e.Exception.StackTrace));
            });
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            MyLog4NetInfo.LogInfo("-----------------------------------------------");
            MyLog4NetInfo.LogInfo(string.Format("程序启动，版本V{0}", version));
        }

        protected void Application_End(object sender, EventArgs e)
        {
            this.RecordEndReason();

            MyLog4NetInfo.LogInfo(string.Format("{0}", "程序结束"));
            MyLog4NetInfo.LogInfo("-----------------------------------------------\r\n\r\n");
        }
        private void RecordEndReason()
        {
            HttpRuntime target = (HttpRuntime)typeof(HttpRuntime).InvokeMember("_theRuntime", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Static, null, null, null);
            if (target != null)
            {
                string str = (string)target.GetType().InvokeMember("_shutDownMessage", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance, null, target, null);
                string str2 = (string)target.GetType().InvokeMember("_shutDownStack", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance, null, target, null);
                MyLog4NetInfo.ErrorInfo(string.Format("\r\n关闭原因：{0}\r\n堆栈信息：{1}", str, str2));
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception baseException = base.Server.GetLastError().GetBaseException();
            //string text1 = "Error Caught in Application_Error event\r\nError in:" + base.Request.Url.ToString() + "\r\nError Message:" + baseException.Message.ToString() + "\r\nStack Trace:" + baseException.StackTrace.ToString();
            //MyLog4NetInfo.ErrorInfo(string.Format("Application_Error程序出错:{0}", baseException.Message));
            //MyLog4NetInfo.ErrorInfo(string.Format("Application_Error程序出错:{0}", baseException.StackTrace));


            // Application_Error中统一处理ajax请求执行中抛出的异常
            // http://www.cnblogs.com/dudu/p/4193541.html
            var baseException = Server.GetLastError();
            if (baseException != null)
            {
                MyLog4NetInfo.LogInfo("Application_Error", baseException);
                if (Request != null && (new HttpRequestWrapper(Request)).IsAjaxRequest())
                {
                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new BaseResult { Code = "5001", Message = baseException.Message, Type = "RESP_SYSError:系统异常" }));
                    Response.Flush();
                    Server.ClearError();
                    return;
                }

                Response.StatusCode = 500;
                Server.ClearError();
            }
        }
    }
}