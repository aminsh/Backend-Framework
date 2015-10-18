using System.Data.Entity;
using Core.DataAccess;
using Core.Domain.Contract;

namespace DataAccess
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public EntityFrameworkUnitOfWork()
        {
            _context = new CoreDbContext();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity, new()
        {
            return new EntityFrameworkRepository<TEntity>(_context);
        }

        public IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class, IEntity, new()
        {
            return new EntityFrameworkReadRepository<TEntity>(_context);
        }
    }
}