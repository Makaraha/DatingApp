using Domain.Interfaces;

namespace Repository.IRepository
{
    public interface IRepository<TEntity, in TKey> 
        where TKey : IEquatable<TKey>
        where TEntity : IEntity<TKey>
    {
        IQueryable<TEntity> GetAllAsQuery();

        Task<TEntity?> GetByIdAsync(TKey id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Remove(TEntity entity);

        Task SaveChangesAsync();
    }
}
