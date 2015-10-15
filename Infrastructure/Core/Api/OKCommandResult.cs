using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Core.Api
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
            var statusCode = result.ValidationResult.IsValid 
                ? HttpStatusCode.OK 
                : HttpStatusCode.InternalServerError;

            var res = _request.CreateResponse(statusCode, new
            {
                Command = result.Command,
                ReturnValue = result.ReturnValue,
                ValidationResult = result.ValidationResult.ToDto()
            });

            return Task.FromResult(res);
        }
    }
}
