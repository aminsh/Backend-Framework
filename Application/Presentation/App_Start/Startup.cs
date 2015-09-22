using System;
using System.Threading.Tasks;
using System.Web.Http;
using Core.Bus;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json.Serialization;
using Owin;
using Utility;

[assembly: OwinStartup(typeof(Presentation.App_Start.Startup))]

namespace Presentation.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account"),
                CookieSecure = CookieSecureOption.SameAsRequest
            });

            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
               new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            //config.Filters.Add(new AuthorizeAttribute());
            var container = IocConfig.Register();
            config.DependencyResolver = new IoCContainer(container);

            app.UseWebApi(config);
            app.MapSignalR();

            RegisterCommands.Execute();
            RegisterEvents.Execute();
        }
    }
}
