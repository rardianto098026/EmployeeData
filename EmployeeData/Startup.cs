using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeeData.Startup))]
namespace EmployeeData
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
