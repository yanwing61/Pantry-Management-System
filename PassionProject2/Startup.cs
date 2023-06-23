using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PassionProject2.Startup))]
namespace PassionProject2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
