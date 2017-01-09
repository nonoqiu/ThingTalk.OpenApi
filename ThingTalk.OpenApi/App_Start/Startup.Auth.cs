using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using ThingTalk.OpenApi.Providers;
using ThingTalk.OpenApi.Models;
using Microsoft.Owin.Cors;
using System.ComponentModel;
using ThingTalk.OpenApi.Application.Interfaces;
using ThingTalk.OpenApi.Repository.Interfaces;
using ThingTalk.OpenApi.Application.Services;
using ThingTalk.OpenApi.Repository.FileStorage;

namespace ThingTalk.OpenApi
{
    public partial class Startup
    {
        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            // Provider 和 RefreshTokenProvider 需使用依赖注入
            //  var containter = IocContainer.Default = new IocUnityContainer();
            // containter.RegisterType<IRefreshTokenService, RefreshTokenService>();
            // containter.RegisterType<IRefreshTokenRepository, RefreshTokenRepository>();

            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new ThingTalkApplicationOAuthProvider(new ClientService(new ClientRepository())),
                RefreshTokenProvider = new ThingTalkPersistenceRefreshTokenProvider(new RefreshTokenService(new RefreshTokenRepository())),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            app.UseCors(CorsOptions.AllowAll);              // 允许跨域
            app.UseOAuthBearerTokens(OAuthOptions);
        }

        #region Old Auth Method
        /*
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }
        /// <summary>
        /// 有关配置身份验证的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301864
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            // 将数据库上下文和用户管理器配置为对每个请求使用单个实例
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // 使应用程序可以使用 Cookie 来存储已登录用户的信息并使用 Cookie 来临时存储有关使用第三方登录提供程序登录的用户的信息
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // 针对基于 OAuth 的流配置应用程序
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                //在生产模式下设 AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // 使应用程序可以使用不记名令牌来验证用户身份
            app.UseOAuthBearerTokens(OAuthOptions);

            // 取消注释以下行可允许使用第三方登录提供程序登录
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
        */
        #endregion
    }
}