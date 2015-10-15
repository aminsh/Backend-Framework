using System.Web;
using System.Web.Http;
using Commands;
using Core.Api;
using Core.Command;
using Queries;
using Utility;

namespace Apis
{
    [RoutePrefix("api/employees")]
    public class EmployeeController : ApiControllerBase
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
        public IHttpActionResult GetAll()
        {
            var request = Request.ToDataSourceRequest();
            return OKQuery(_empployeeQuery.All(request));
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            return OKQuery(_empployeeQuery.ById(id));
        }

        [Authorize]
        [Route("")]
        [HttpPost]
        public IHttpActionResult Create(CreateEmployeeCommand cmd)
        {
            var user = HttpContext.Current.User;
            _commandBus.Send(cmd);
            return OKCommnad();
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Update(UpdateEmployeeCommand cmd, int id)
        {
            cmd.Id = id;
            _commandBus.Send(cmd);
            return OKCommnad();
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            _commandBus.Send(new RemoveEmployeeCommand {Id = id});
            return OKCommnad();
        }
    }
}