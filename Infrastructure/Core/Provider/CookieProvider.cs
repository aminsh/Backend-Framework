using System.Web;

namespace Core.Provider
{
    public interface ICookieProvider
    {
        void Set(string key, object value);

        string Get(string key);
    }

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