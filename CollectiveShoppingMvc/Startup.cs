using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CollectiveShoppingMvc.Startup))]
namespace CollectiveShoppingMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
