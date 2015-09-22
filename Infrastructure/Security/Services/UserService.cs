using System;
using System.Linq;
using System.Web.Security;
using Core.ApiResult;
using Core.DataAccess;
using DataAccess;
using Domain;

namespace Security.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidationResult _validationResult; 

        public UserService(
            IRepository<User> userRepository, 
            IRepository<Employee> employeeRepository,
            IUnitOfWork unitOfWork,
            IValidationResult validationResult)
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _validationResult = validationResult;
        }

        public void RegisterByName(string userName, string password, string name)
        {
            if(_userRepository.Query().Any(u=> u.UserName.ToLower() == userName.ToLower()))
                _validationResult.AddError("نام کاربری تکراری میباشد");

            if(!_validationResult.IsValid)
                return;

            var encryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            var newUser = new User
            {
                Id =  Guid.NewGuid(),
                Name = name,
                Password = encryptedPassword,
                UserName = userName
            };

            _userRepository.Add(newUser);
            _unitOfWork.Commit();
        }

        public void ReqisterByEmployee(string userName, string password, Guid employeeId)
        {
            if (_userRepository.Query().Any(u=> u.Employee.Id == employeeId))
                _validationResult.AddError("کابری با شماره پرسنلی وارد شده قبلا ثبت نام کرده");

            if (!_validationResult.IsValid)
                return;

            var employee = _employeeRepository.FindById(employeeId);
            var encryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Password = encryptedPassword,
                Name = employee.FirstName + " " + employee.LastName,
                UserName = userName,
                Employee = employee
            };

            _userRepository.Add(newUser);
            _unitOfWork.Commit();
        }

        public void ChangePassword(int userId, string password)
        {
            var user = _userRepository.FindById(userId);
            var encryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");

            user.Password = encryptedPassword;

            _unitOfWork.Commit();
        }
    }
}
