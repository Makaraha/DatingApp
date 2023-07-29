using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Translations.Base
{
    public class BaseTranslation : ITranslation.ITranslation
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        public string LocalizedName { get; set; }

        [MaxLength(16)]
        public string CultureName { get; set; }
    }
}
