using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ThingTalk.OpenApi.Startup))]

namespace ThingTalk.OpenApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}