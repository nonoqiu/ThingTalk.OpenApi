using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(ThingTalk.OpenApi.Startup))]

namespace ThingTalk.OpenApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true      // 跨域的关键语句
                };
                map.RunSignalR(hubConfiguration);
            });
            app.MapSignalR();

            ConfigureAuth(app);
        }
    }
}