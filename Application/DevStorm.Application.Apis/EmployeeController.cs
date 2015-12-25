using System;
using System.Web;
using System.Web.Http;
using DevStorm.Application.Commands;
using DevStorm.Application.Queries;
//using DevStorm.Infrastructure.Api;
using DevStorm.Infrastructure.Api;
using DevStorm.Infrastructure.Core.CQRS;
using DevStorm.Infrastructure.Utility;

namespace DevStorm.Application.Apis
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
        public IHttpActionResult GetById(Guid id)
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