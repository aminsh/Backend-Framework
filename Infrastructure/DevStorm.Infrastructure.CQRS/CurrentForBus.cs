using System;
using DevStorm.Infrastructure.Core.Domain;

namespace DevStorm.Infrastructure.CQRS
{
    public class CurrentForBus : ICurrent
    {
        public Guid UserId { get; set; }
        public Guid PeriodId { get; set; }
        public Guid BranchId { get; set; }
    }
}