using System.Linq;

namespace DevStorm.Infrastructure.Core.DataAccess
{
    public interface IReadStorage
    {
        IQueryable<TView> Get<TView>() where TView : class, new();

        TView GetById<TView>(object id) where TView : class, new();
    }
}