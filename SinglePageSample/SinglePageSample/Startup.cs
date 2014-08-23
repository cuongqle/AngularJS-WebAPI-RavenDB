using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SinglePageSample.Startup))]
namespace SinglePageSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
