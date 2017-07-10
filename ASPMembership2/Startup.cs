using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPMembership2.Startup))]
namespace ASPMembership2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
