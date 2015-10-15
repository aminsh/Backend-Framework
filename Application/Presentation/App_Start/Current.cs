using System;
using System.Web;
using Core.Domain.Contract;
using Utility;

namespace Presentation.App_Start
{
    public class Current : ICurrent
    {
        public Guid UserId
        {
            get { return Guid.Parse(HttpContext.Current.User.Identity.Name); }
        }

        public Guid PeriodId
        {
            get { return HttpContext.Current.GetOwinContext().Request.Cookies["period-id"].Convert<Guid>(); }
        }

        public Guid BranchId
        {
            get { return HttpContext.Current.GetOwinContext().Request.Cookies["branch-id"].Convert<Guid>(); }
        }
    }
}