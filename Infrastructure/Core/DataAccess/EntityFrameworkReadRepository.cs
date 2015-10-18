using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Core.Domain.Contract;

namespace Core.DataAccess
{
    public class EntityFrameworkReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _context;

        public EntityFrameworkReadRepository(DbContext context)
        {
            _context = context;
        }

        public TEntity GetById(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().FirstOrDefault(expression);
        }

        public IQueryable<TEntity> Get()
        {
            return _context.Set<TEntity>();
        }
    }
}