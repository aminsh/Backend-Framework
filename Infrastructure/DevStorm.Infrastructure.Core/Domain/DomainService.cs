using DevStorm.Infrastructure.Core.CQRS;
using DevStorm.Infrastructure.Core.DataAccess;

namespace DevStorm.Infrastructure.Core.Domain
{
    public class DomainService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IEventBus EventBus { get; set; }
        public ICurrent Current { get; set; }
        public object ReturnValue { get; set; }
    }
}
