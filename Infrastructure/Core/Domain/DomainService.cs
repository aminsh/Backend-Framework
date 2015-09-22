using Core.ApiResult;
using Core.DataAccess;
using Core.Domain.Contract;
using Core.Event;
using Utility;

namespace Core.Domain
{
    public class DomainService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IEventBus EventBus { get; set; }
        public ICurrent Current { get; set; }
        protected IValidationResult ValidationResult { get; set; }
    }
}
