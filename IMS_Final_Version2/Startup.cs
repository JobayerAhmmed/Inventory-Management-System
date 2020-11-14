using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IMS_Final_Version2.Startup))]
namespace IMS_Final_Version2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
