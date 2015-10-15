using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Core.Api
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
