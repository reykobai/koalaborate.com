using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(themebitch.Startup))]
namespace themebitch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
