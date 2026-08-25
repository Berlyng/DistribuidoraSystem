using System;
using System.Collections.Generic;
using System.Text;

namespace Distribuidora.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        public DateTime? DeletedAt { get; protected set; }


        protected BaseEntity() { 
            
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
        
        public bool isDelete => DeletedAt.HasValue;

    }
}
