using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Historical_tour.Startup))]
namespace Historical_tour
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
