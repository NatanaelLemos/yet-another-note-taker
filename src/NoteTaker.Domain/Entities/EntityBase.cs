using System;
using System.Collections.Generic;
using System.Text;

namespace NoteTaker.Domain.Entities
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
            Available = true;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }

        public Guid Id { get; set; }

        public bool Available { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }
    }
}
