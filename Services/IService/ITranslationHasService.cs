using Domain.Interfaces;
using Mapster;

namespace Services.IService
{
    public interface ITranslationHasService<TEntity> : IService<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        Task AddOrEditTranslation<TDto>(TDto dto, int entityId, TypeAdapterConfig? cnf = null);
    }
}
