using System;

namespace Core.Domain.Contract
{
    public interface ICurrent
    {
        Guid UserId { get; }
        int PeriodId { get; }
        int StockId { get; }
    }
}

    
