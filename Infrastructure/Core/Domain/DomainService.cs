using Core.Bus;
using Core.DataAccess;
using Core.Domain.Contract;

namespace Core.Domain
{
    public class DomainService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IEventBus EventBus { get; set; }
        public ICurrent Current { get; set; }
        public object ReturnValue { get; set; }
    }
}
