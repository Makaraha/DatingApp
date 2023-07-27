using Common.Enums;
using System.ComponentModel.DataAnnotations;

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

        public GenderEnum Gender { get; set; }

        public GenderEnum SearchingGender { get; set; }

        [MaxLength(512)]
        public string About { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
