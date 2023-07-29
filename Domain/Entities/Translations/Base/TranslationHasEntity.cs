using System.Globalization;

namespace Domain.Entities.Translations.Base
{
    public class TranslationHasEntity<TTranslation> : BaseEntity
        where TTranslation : ITranslation.ITranslation
    {
        public string Name { get; set; }

        public HashSet<TTranslation> Translations { get; set; } = new HashSet<TTranslation>();

        public string GetLocalizedName()
        {
            return Translations?.FirstOrDefault(x => x.CultureName == CultureInfo.CurrentCulture.Name)
                ?.LocalizedName ?? Name;
        }
    }
}
