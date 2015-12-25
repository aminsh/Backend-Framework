using DevStorm.Infrastructure.Core.Api;

namespace DevStorm.Infrastructure.Core.CQRS
{
    public interface ISendCommandToValidator
    {
        IValidationResult Validate(CommnadMessage message);
    }
}
