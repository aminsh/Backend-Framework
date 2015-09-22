using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Utility
{
    public static class HttpExtension
    {
        public static HttpResponseMessage OK(this HttpRequestMessage request)
        {
            return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
