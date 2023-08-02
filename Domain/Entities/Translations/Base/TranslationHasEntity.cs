using Domain.Infrastructure;

namespace Domain.Entities.Translations.Base
{
    public class TranslationHasEntity<TTranslation> : BaseEntity
        where TTranslation : ITranslation.ITranslation
    {
        public string Name { get; set; }

        public TranslationsCollection<TTranslation> Translations { get; set; } 
            = new TranslationsCollection<TTranslation>();
    }
}
