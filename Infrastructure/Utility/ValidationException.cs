using System;

namespace Utility
{
    public class ValidationException : Exception
    {
        public object validationErrors { get; set; }
    }
}
