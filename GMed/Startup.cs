using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GMed.Startup))]
namespace GMed
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
