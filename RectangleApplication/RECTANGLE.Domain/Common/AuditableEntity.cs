using System;
using System.ComponentModel.DataAnnotations;
using Rectangle.Domain.Entities;

namespace Rectangle.Domain.Common
{
    public class AuditableEntity
    {
        public Guid CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? LastModifiedById { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
