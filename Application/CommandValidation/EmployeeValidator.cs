using Commands;
using Core.Command;
using Core.DataAccess;
using Core.Domain;
using Domain;

namespace CommandValidation
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
