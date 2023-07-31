using System.Collections.ObjectModel;
using System.Globalization;
using Domain.Entities.Translations.ITranslation;

namespace Domain.Infrastructure
{
    public class TranslationsCollection<T> : Collection<T>
        where T : ITranslation
    {
        public string? GetLocalizedName()
        {
            return GetPropertyValue(nameof(ITranslation.LocalizedName));
        }

        public string? GetPropertyValue(string propertyName)
        {
            return GetPropertyValue(CultureInfo.CurrentCulture.Name, propertyName);
        }

        public string? GetPropertyValue(string culture, string propertyName)
        {
            var translation = this.FirstOrDefault(x => x.CultureName == culture);
            if (translation == null) 
                return null;

            var type = translation.GetType();
            return type.GetProperty(propertyName)?.GetValue(translation)?.ToString();
        }

        public bool HasCulture(string culture)
        {
            return this.Any(x => x.CultureName == culture);
        }
    }
}
