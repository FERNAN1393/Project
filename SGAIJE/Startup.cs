using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SGAIJE.Startup))]
namespace SGAIJE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
