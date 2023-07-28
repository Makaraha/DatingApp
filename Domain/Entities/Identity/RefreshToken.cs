using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public class RefreshToken : BaseEntity
    {

        public string UserName { get; set; }

        [Required, MaxLength(64)]
        public string Token { get; set; }

    }
}
