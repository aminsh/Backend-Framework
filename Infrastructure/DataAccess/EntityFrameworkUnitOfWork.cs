using System.Data.Entity;
using Core.DataAccess;

namespace DataAccess
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public string temp { get; set; }
        public EntityFrameworkUnitOfWork()
        {
            _context = new AppDbContext();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class , new()
        {
            return new EntityFrameworkRepository<TEntity>(_context);
        }

        public IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class, new()
        {
            return new EntityFrameworkReadRepository<TEntity>(_context);
        }

    }
}
