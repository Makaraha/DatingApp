using System.ComponentModel.DataAnnotations;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class BaseEntity : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public DateTime LastModifiedDateUtc { get; set; }

        public bool IsDeleted { get; set; }
    }
}
