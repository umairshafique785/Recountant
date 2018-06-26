using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReCountant.Startup))]
namespace ReCountant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
