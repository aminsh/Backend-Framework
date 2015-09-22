using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Commands;
using Core.Command;
using Kendo.DynamicLinq;
using Queries;
using Utility;

namespace Apis
{
    [RoutePrefix("api/employees")]
    public class EmployeeController : ApiController
    {
        private readonly ICommandBus _commandBus;
        private readonly EmployeeQuery _empployeeQuery;

        public EmployeeController(ICommandBus commandBus, EmployeeQuery employeeQuery)
        {
            
            _commandBus = commandBus;
            _empployeeQuery = employeeQuery;
        }

        [Route("")]
        [HttpGet]
        public DataSourceResult GetAll()
        {
                var request = Request.ToDataSourceRequest();
            return _empployeeQuery.All(request);
        }

        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
           return  Request.CreateResponse(_empployeeQuery.ById(id));
        }

        [Authorize]
        [Route("")]
        [HttpPost]
        public HttpResponseMessage Create(CreateEmployeeCommand cmd)
        {
            var user = HttpContext.Current.User;
            _commandBus.Send(cmd);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}")]
        [HttpPut]
        public HttpResponseMessage Update(UpdateEmployeeCommand cmd, int id)
        {
            cmd.Id = id;
            _commandBus.Send(cmd);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage Remove(int id)
        {
            _commandBus.Send(new RemoveEmployeeCommand{Id = id});
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
