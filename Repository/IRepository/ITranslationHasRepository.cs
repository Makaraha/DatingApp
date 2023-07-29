using Domain.Entities.Translations.Base;
using Domain.Entities.Translations.ITranslation;

namespace Repository.IRepository
{
    public interface ITranslationHasRepository<TEntity, TTranslation> : IRepository<TEntity, int>
        where TTranslation : class, ITranslation
        where TEntity : TranslationHasEntity<TTranslation>
    {
        void AddTranslation(TTranslation translation, TEntity entity);

        Task<IEnumerable<TTranslation>?> GetTranslationsAsync(int entityId);
    }
}
