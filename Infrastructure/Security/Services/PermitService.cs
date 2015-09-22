//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Core;
//using DataAccess.Data;
//using Domain.Model;
//using Presentation.DTO.Commands;
//using Security.Model;

//namespace Security.Service
//{
//    public class PermitService
//    {
//        private readonly IRepository<User> _userRepository;
//        private readonly IRepository<UserPermit> _userPermitRepository;
//        private readonly IRepository<Subject> _subjectRepository;
//        private readonly AuthenticationService _authenticationService;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IValidationResult _validationResult; 

//        public PermitService(
//            IRepository<User> userRepository, 
//            IRepository<UserPermit> userPermitRepository,
//            IRepository<Subject> subjectRepository,
//            AuthenticationService authenticationService,
//            IUnitOfWork unitOfWork,
//            IValidationResult ValidationResult)
//        {
//            _userRepository = userRepository;
//            _userPermitRepository = userPermitRepository;
//            _authenticationService = authenticationService;
//            _subjectRepository = subjectRepository;
//            _unitOfWork = unitOfWork;
//            _validationResult = ValidationResult;
//        }

//        public void UpdateUserPermit(UpdateUserPermitCommand cmd)
//        {
//            var userPermits = _userPermitRepository.ReadStorage().Where(up => up.User.Id == cmd.UserId)
//                .ToList();

//            userPermits.ForEach(up => _userPermitRepository.Delete(up));
//            cmd.Subjects.ToList().ForEach(s =>
//            {
//                var user = _userRepository.FindById(cmd.UserId);
//                var subject = _subjectRepository.FindById(s);

//                var permit = new UserPermit
//                {
//                    User = user,
//                    Subject = subject,
//                    IsAllowed = true
//                };

//                _userPermitRepository.Add(permit);
//            });

//            _unitOfWork.Commit();

//           _authenticationService.RemoveToken(cmd.UserId);
//        }
//    }
//}
