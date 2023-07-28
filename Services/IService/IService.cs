using Common.Interfaces;
using Domain.Interfaces;
using Mapster;

namespace Services.IService
{
    public interface IService<TEntity, TKey>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task<TDto?> GetByIdAsync<TDto>(TKey id, TypeAdapterConfig? cnf = null);

        Task<TKey> AddAsync<TDto>(TDto dto, TypeAdapterConfig? cnf = null);

        Task Update<TDto>(TDto dto, TypeAdapterConfig? cnf = null)
            where TDto : IIdHas<TKey>;

        Task Delete(TKey id);

        Task Remove(TKey id);

        Task RemoveRange(IEnumerable<TEntity> entities);
    }
}
