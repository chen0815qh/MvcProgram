using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcProgram1.Startup))]
namespace MvcProgram1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
