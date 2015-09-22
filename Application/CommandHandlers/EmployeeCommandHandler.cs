﻿using Commands;
using Core.Command;
using Core.DataAccess;
using Core.Domain;
using Domain;
using Hubs;

namespace CommandHandlers
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

        }

        public void Handle(UpdateEmployeeCommand cmd)
        {
            var employee = _employeeRepository.FindById(cmd.Id);

            employee.FirstName = cmd.FirstName;
            employee.LastName = cmd.LastName;

            if (ValidationResult.IsValid)
                UnitOfWork.Commit();
        }

        public void Handle(RemoveEmployeeCommand cmd)
        {
            var employee = _employeeRepository.FindById(cmd.Id);

            _employeeRepository.Delete(employee);

            UnitOfWork.Commit();

        }
    }
}
