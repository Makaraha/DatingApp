using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public class User : BaseUserIdentity
    {
        [MaxLength(64)]
        public string FirstName { get; set; }

        [MaxLength(64)]
        public string LastName { get; set; }

        [MaxLength(64)]
        public string City { get; set; }

        [ForeignKey(nameof(Gender))]
        public int GenderId { get; set; }

        [ForeignKey(nameof(SearchingGender))]
        public int SearchingGenderId { get; set; }

        [MaxLength(512)]
        public string About { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public Gender SearchingGender { get; set; }
    }
}
