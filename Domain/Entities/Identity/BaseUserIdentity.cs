using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class BaseUserIdentity : IdentityUser<int>
    {
        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
