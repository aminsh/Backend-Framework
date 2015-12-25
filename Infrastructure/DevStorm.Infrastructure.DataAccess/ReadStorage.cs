using System.Linq;
using DevStorm.Infrastructure.Core.DataAccess;

namespace DevStorm.Infrastructure.DataAccess
{
    public class ReadStorage : IReadStorage
    {
        private readonly ReadContext _context;

        public ReadStorage(ReadContext context)
        {
            _context = context;
        }

        public IQueryable<TView> Get<TView>() where TView : class, new()
        {
            return _context.Set<TView>().AsNoTracking();
        }

        public TView GetById<TView>(object id) where TView : class, new()
        {
            return _context.Set<TView>().Find(id);
        }
    }
}