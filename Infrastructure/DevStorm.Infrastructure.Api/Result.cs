using DevStorm.Infrastructure.Core.Api;

namespace DevStorm.Infrastructure.Api
{
    public class Result : IResult
    {
        public object Command { get; set; }
        public object ReturnValue { get; set; }
        public IValidationResult ValidationResult { get; set; }
    }
}
