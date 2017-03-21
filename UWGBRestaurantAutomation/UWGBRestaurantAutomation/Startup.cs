using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UWGBRestaurantAutomation.Startup))]
namespace UWGBRestaurantAutomation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
