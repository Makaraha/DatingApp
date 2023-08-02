using Domain.Entities.Identity;
using Domain.Entities.Translations;
using Domain.Entities.Translations.Base;

namespace Domain.Entities
{
    public class Interest : TranslationHasEntity<InterestTranslation>
    {
        public ICollection<User> Users { get; set; }
    }
}
