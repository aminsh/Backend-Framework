using System.Data.Entity;
using DevStorm.Infrastructure.Core.DataAccess;
using DevStorm.Infrastructure.Core.Domain;


namespace DevStorm.Infrastructure.DataAccess
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public EntityFrameworkUnitOfWork()
        {
            _context = new AppDbContext();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity, new()
        {
            return new EntityFrameworkRepository<TEntity>(_context);
        }
    }
}