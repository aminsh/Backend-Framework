using DevStorm.Application.Commands;
using DevStorm.Application.Domain;
using DevStorm.Infrastructure.Core.CQRS;
using DevStorm.Infrastructure.Core.DataAccess;
using DevStorm.Infrastructure.Core.Domain;

namespace DevStorm.Application.CommandValidations
{
    public class EmployeeValidator : DomainValidator,
        ICommandValidator<CreateEmployeeCommand>
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeValidator(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Validate(CreateEmployeeCommand cmd)
        {
            if(cmd.FirstName.Length < 3)
                ValidationResult.AddError("نام باید بیشتر 3 کاراکتر باشد");

            
        }
    }
}
