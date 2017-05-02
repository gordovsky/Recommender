using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Recommender_final.Startup))]
namespace Recommender_final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
