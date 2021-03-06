﻿
namespace DevStorm.Infrastructure.Core.Api
{
    public interface IResult
    {
        object Command { get; set; }
        object ReturnValue { get; set; }
        IValidationResult ValidationResult { get; set; }
    }
}
