using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEntity<out TKey>
        where TKey : IEquatable<TKey>
    {
        TKey Id { get; }

        public DateTime CreatedDateUtc { get; set; }

        public DateTime LastModifiedDateUtc { get; set; }

        public bool IsDeleted { get; set; }
    }
}
