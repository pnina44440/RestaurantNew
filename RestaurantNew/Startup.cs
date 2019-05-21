using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestaurantNew.Startup))]
namespace RestaurantNew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
