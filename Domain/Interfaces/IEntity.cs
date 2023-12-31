﻿namespace Domain.Interfaces
{
    public interface IEntity<out TKey>
        where TKey : IEquatable<TKey>
    {
        TKey Id { get; }

        public DateTime CreatedDateUtc { get; set; }

        public DateTime LastModifiedDateUtc { get; set; }

        bool IsDeleted { get; set; }
    }
}
