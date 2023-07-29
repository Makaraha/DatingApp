namespace Domain.Entities.Translations.Base
{
    public class TranslationHasEntity<TTranslation> : BaseEntity
        where TTranslation : ITranslation.ITranslation
    {
        public string Name { get; set; }

        public HashSet<TTranslation> Translations { get; set; } = new HashSet<TTranslation>();
    }
}
