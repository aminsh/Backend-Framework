using DevStorm.Infrastructure.Core.Api;

namespace DevStorm.Infrastructure.Core.Domain
{
    public class DomainValidator
    {
        public ICurrent Current { get; set; }
        public IValidationResult ValidationResult { get; set; }
    }
}
