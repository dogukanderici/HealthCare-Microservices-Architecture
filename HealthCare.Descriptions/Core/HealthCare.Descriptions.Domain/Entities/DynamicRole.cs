using HealthCare.Descriptions.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Domain.Entities
{
    public class DynamicRole : IEntity
    {
        public Guid Id { get; set; }
        public Guid DynamicPolicyId { get; set; }
        public Guid AllowedRoleId { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }

        // Relations
        [ForeignKey(nameof(DynamicPolicyId))]
        public DynamicPolicy DynamicPolicy { get; set; }

        [ForeignKey(nameof(AllowedRoleId))]
        public SyncedRole SyncedRole { get; set; }
    }
}