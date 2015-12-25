using System.Web;
using DevStorm.Infrastructure.Core.Api;

namespace DevStorm.Infrastructure.Api
{

    public class CookieProvider : ICookieProvider
    {
        public void Set(string key, object value)
        {
            HttpContext.Current.GetOwinContext().Response.Cookies.Append(key, value.ToString());
        }

        public string Get(string key)
        {
            return HttpContext.Current.GetOwinContext().Request.Cookies[key];
        }
    }
}