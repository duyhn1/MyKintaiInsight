using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mykintai_Indentỉy.Startup))]
namespace Mykintai_Indentỉy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
