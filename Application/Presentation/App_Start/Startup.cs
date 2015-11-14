using Core.IOC;
using Core.Owin;
using Microsoft.Owin;
using Owin;
using Utility;

[assembly: OwinStartup(typeof(Presentation.App_Start.Startup))]

namespace Presentation.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IOCConfiguration.Register();

            OwinConfiguration.Register(
                app , 
                new IoCContainer(DependencyManager.Container));
        }
    }
}
