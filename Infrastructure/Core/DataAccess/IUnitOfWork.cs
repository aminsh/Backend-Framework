

namespace Core.DataAccess
{
    public interface IUnitOfWork
    {
        void Commit();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class , new();
        IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class, new();
    }
}
