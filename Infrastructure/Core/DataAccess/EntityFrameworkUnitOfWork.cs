using System.Data.Entity;
using Core.Domain.Contract;
using DataAccess;

namespace Core.DataAccess
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