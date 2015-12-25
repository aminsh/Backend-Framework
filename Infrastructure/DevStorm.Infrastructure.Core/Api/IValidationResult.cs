using System.Collections.Generic;

namespace DevStorm.Infrastructure.Core.Api
{
    public interface IValidationResult
    {
        IList<Error> Errors { get; set; }
        bool IsValid { get; }
        bool IsNotValid { get; }
        void AddError(string message);
        void AddError(string message, string propertyName);
        object ToDto();
    }
}
