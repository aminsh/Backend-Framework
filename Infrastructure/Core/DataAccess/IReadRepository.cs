using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Domain.Contract;

namespace Core.DataAccess
{
    public interface IReadRepository<TEntity> where TEntity : class, IEntity
    {
        TEntity GetById(object id);
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> Get();
    }
}