using System;
using System.Web;
using Core.Domain.Contract;

namespace Presentation
{
    public class Current : ICurrent
    {
        public Guid UserId { get { return Guid.Parse(HttpContext.Current.User.Identity.Name); } }
        public int PeriodId { get { return 12; } }
        public int StockId { get { return 4; } }
    }
}
