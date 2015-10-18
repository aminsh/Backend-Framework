using Core.Domain.Contract;

namespace Core.DataAccess
{
    public interface IUnitOfWork
    {
        void Commit();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity, new();
        IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class, IEntity, new();
    }
}