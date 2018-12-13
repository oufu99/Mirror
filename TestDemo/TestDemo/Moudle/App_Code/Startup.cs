using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Moudle.Startup))]
namespace Moudle
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
