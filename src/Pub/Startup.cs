using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pub.Startup))]
namespace Pub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
