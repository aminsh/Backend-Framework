using System;

namespace DevStorm.Infrastructure.Core.Domain
{
    public interface ICurrent
    {
        Guid UserId { get; }
        Guid PeriodId { get; }
        Guid BranchId { get; }
    }
}