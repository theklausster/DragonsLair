using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DragonLairFrontEnd.Startup))]
namespace DragonLairFrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
