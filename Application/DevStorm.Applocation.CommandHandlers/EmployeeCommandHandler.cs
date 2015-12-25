using DevStorm.Application.Commands;
using DevStorm.Application.Domain;
using DevStorm.Infrastructure.Core.CQRS;
using DevStorm.Infrastructure.Core.DataAccess;
using DevStorm.Infrastructure.Core.Domain;
using Domain;

namespace DevStorm.Applocation.CommandHandlers
{
    public class EmployeeCommandHandler : DomainService,
        ICommandHandler<CreateEmployeeCommand>,
        ICommandHandler<UpdateEmployeeCommand>,
        ICommandHandler<RemoveEmployeeCommand>
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<User> _userRepository; 
        
        public EmployeeCommandHandler(
            IRepository<Employee> employeeRepository,
            IRepository<User> userRepository)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
        }

        public void Handle(CreateEmployeeCommand cmd)
        {
            var newEmployee = new Employee
            {
                FirstName = cmd.FirstName,
                LastName = cmd.LastName
            };

            _employeeRepository.Add(newEmployee);
            UnitOfWork.Commit();

            ReturnValue = newEmployee;
        }

        public void Handle(UpdateEmployeeCommand cmd)
        {
            var employee = _employeeRepository.FindById(cmd.Id);

            employee.FirstName = cmd.FirstName;
            employee.LastName = cmd.LastName;

            ReturnValue = new {};
        }

        public void Handle(RemoveEmployeeCommand cmd)
        {
            var employee = _employeeRepository.FindById(cmd.Id);

            _employeeRepository.Delete(employee);

            UnitOfWork.Commit();

        }
    }
}
