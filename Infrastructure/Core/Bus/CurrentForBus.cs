using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Contract;

namespace Core.Bus
{
    public class CurrentForBus : ICurrent
    {
        public Guid UserId { get; set; }
        public int PeriodId { get; set; }
        public int StockId { get; set; }
    }
}
