using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Translations.Base;

namespace Domain.Entities.Translations
{
    public class GenderTranslation : BaseTranslation
    {
        [ForeignKey(nameof(Gender))]
        public int GenderId { get; set; }

        public Gender Gender { get; set; }
    }
}
