using System.Web.Http;

namespace DevStorm.Infrastructure.Api
{
    public class ApiControllerBase : ApiController
    {
        public IHttpActionResult OKCommnad()
        {
            return  new OKCommandResult(Request);
        }

        public IHttpActionResult OKQuery(object data)
        {
            return new OKQueryResult(Request, data);
        }
    }
}
