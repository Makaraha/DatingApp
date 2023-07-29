namespace Domain.Entities.Translations.ITranslation
{
    public interface ITranslation
    {
        string LocalizedName { get; set; }

        string CultureName { get; set; }
    }
}
