using Core.ApiResult;

namespace Core.Api
{
    public class Result : IResult
    {
        public object Command { get; set; }
        public object ReturnValue { get; set; }
        public IValidationResult ValidationResult { get; set; }
    }
}
