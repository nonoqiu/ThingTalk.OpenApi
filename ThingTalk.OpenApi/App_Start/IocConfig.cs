using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using ThingTalk.OpenApi.Application.Interfaces;
using ThingTalk.OpenApi.Application.Services;
using ThingTalk.OpenApi.Providers;
using ThingTalk.OpenApi.Repository.FileStorage;
using ThingTalk.OpenApi.Repository.Interfaces;

namespace ThingTalk.OpenApi.App_Start
{
    /// <summary>
    /// 依赖注入配置类
    /// </summary>
    public class IocConfig
    {
        /// <summary>
        /// 初始化依赖配置项
        /// 注册类别参考：http://www.cnblogs.com/jys509/p/4649798.html
        /// </summary>
        public static void RegisterDependencies()
        {
            SetAutofacWebAPI();
        }
        private static void SetAutofacWebAPI()
        {
            var builder = new ContainerBuilder();

            // Register controllers using assembly scanning. 
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // Register API controllers using assembly scanning. 
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<RefreshTokenRepository>().As<IRefreshTokenRepository>().SingleInstance();
            builder.RegisterType<ClientRepository>().As<IClientRepository>();

            builder.RegisterType<ClientService>().As<IClientService>();
            builder.RegisterType<RefreshTokenService>().As<IRefreshTokenService>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

            builder.RegisterType<ThingTalkApplicationOAuthProvider>().As<OAuthAuthorizationServerProvider>();
            builder.RegisterType<ThingTalkPersistenceRefreshTokenProvider>().As<AuthenticationTokenProvider>();

            // 重新构造IDocumentationProvider, 以防止注册WebAPI时GlobalConfiguration.Configuration.DependencyResolver被更改导致IDocumentationProvider丢失
            //builder.RegisterType<XmlDocumentationProvider>().As<IDocumentationProvider>().SingleInstance().PreserveExistingDefaults();

            IContainer container = builder.Build();

            // Set the dependency resolver for Web API.
            // http://www.cnblogs.com/zhouruifu/archive/2012/04/03/dependency-injection-in-asp-net-web-api-using-autofac.html
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

            // Set the dependency resolver for MVC.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}