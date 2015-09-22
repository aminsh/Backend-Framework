using System;
using System.Collections.Generic;
using System.Linq;
using Core.ApiResult;

namespace Utility
{
    public class ValidationResult : IValidationResult
    {
        public ValidationResult()
        {
            Errors = new List<Error>();
        }

        public IList<Error> Errors { get; set; }

        public bool IsValid
        {
            get { return !Errors.Any(); }
        }

        public bool IsNotValid
        {
            get { return Errors.Any(); }
        }

        public void AddError(string message)
        {
            Errors.Add(new Error {Message = message});
        }

        public void AddError(string message, string propertyName)
        {
            Errors.Add(new Error {Message = message, PropertyName = propertyName});
        }

        public object ToDto()
        {
            return new
            {
                isValid = IsValid,
                errors = Errors.Select(e => e.Message).ToList()
            };
        }
    }
}