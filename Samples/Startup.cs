using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Samples.Startup))]
namespace Samples
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
