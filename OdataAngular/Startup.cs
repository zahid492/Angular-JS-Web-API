using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OdataAngular.Startup))]
namespace OdataAngular
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
