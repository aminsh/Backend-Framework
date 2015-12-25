namespace DevStorm.Infrastructure.Core.Api
{
    public interface ICookieProvider
    {
        void Set(string key, object value);

        string Get(string key);
    }
}