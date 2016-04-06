using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CS401WebApp.Startup))]
namespace CS401WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
