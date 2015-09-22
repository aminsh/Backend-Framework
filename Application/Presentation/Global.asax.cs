using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace Presentation
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
