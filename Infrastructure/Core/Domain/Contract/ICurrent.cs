using System;

namespace Core.Domain.Contract
{
    public interface ICurrent
    {
        Guid UserId { get; }
        Guid PeriodId { get; }
        Guid BranchId { get; }
    }
}