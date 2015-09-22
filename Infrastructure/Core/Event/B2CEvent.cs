using System;
using Core.ApiResult;
using Core.Command;

namespace Core.Event
{
    public class B2CEvent
    {
        public Guid EventId { get; set; }
        public ICommand Command { get; set; }
        public IValidationResult ValidationResult { get; set; }
    }
}
