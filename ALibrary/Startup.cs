using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ALibrary.Startup))]
namespace ALibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
