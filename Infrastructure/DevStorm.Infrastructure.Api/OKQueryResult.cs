using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DevStorm.Infrastructure.Api
{
    public class OKQueryResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;
        private readonly object _data;

        public OKQueryResult(HttpRequestMessage request, object data)
        {
            _request = request;
            _data = data;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var res = _request.CreateResponse(HttpStatusCode.OK, _data);

            return Task.FromResult(res);
        }
    }
}