using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlatformTestApplication.Startup))]
namespace PlatformTestApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
