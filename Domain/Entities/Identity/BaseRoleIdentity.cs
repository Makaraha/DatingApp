using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class BaseRoleIdentity : IdentityRole<int>, IEntity<int>
    {
        public DateTime CreatedDateUtc { get; set; }

        public DateTime LastModifiedDateUtc { get; set; }

        public bool IsDeleted { get; set; }
    }
}
