using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudyScraper.Web.Startup))]
namespace StudyScraper.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
