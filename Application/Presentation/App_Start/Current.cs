using System;
using System.Web;
using Core.Domain.Contract;
using Core.IOC;
using Core.Provider;

namespace Presentation.App_Start
{
    public class Current : ICurrent
    {
        public Guid UserId
        {
            get
            {
                return Guid.Parse(HttpContext.Current.User.Identity.Name);
            }
        }

        public Guid PeriodId
        {
            get
            {
                return Guid.Parse(DependencyManager.Resolve<ICookieProvider>().Get("period-id"));
            }
        }

        public Guid BranchId
        {
            get
            {
                return Guid.Parse(DependencyManager.Resolve<ICookieProvider>().Get("branch-id"));
            }
        }
    }
}