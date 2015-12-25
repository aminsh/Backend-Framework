using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json.Serialization;
using Owin;

namespace DevStorm.Infrastructure.Api
{
    public static class  OwinConfiguration
    {
        public static void Register(IAppBuilder app, IDependencyResolver resolver)
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

            config.Filters.Add(new AuthorizeAttribute());
            config.DependencyResolver = resolver;
            
            
            app.UseWebApi(config);
            app.MapSignalR();
        }
    }
}
