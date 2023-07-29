using Domain.EF.Context;
using Domain.Entities.Translations.Base;
using Domain.Entities.Translations.ITranslation;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class TranslationHasRepository<TEntity, TTranslation> : BaseRepository<TEntity, int>
        where TTranslation : class, ITranslation
        where TEntity : TranslationHasEntity<TTranslation>
    {
        public TranslationHasRepository(ApplicationDbContext context) : base(context)
        {
        }

        public virtual void AddTranslation(TTranslation translation, TEntity entity)
        {
            entity.Translations.Add(translation);
        }
    }
}
