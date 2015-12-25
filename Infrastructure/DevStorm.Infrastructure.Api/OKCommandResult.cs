using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using DevStorm.Infrastructure.Core.Api;
using DevStorm.Infrastructure.Core.IOC;

namespace DevStorm.Infrastructure.Api
{
    public class OKCommandResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;

        public OKCommandResult(HttpRequestMessage request)
        {
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var result = DependencyManager.Resolve<IResult>();

            var res = _request.CreateResponse(HttpStatusCode.OK, new
            {
                Command = result.Command,
                ReturnValue = result.ReturnValue,
                ValidationResult = result.ValidationResult.ToDto()
            });

            return Task.FromResult(res);
        }
    }
}
