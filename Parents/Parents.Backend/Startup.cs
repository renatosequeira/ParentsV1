using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Parents.Backend.Startup))]
namespace Parents.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
