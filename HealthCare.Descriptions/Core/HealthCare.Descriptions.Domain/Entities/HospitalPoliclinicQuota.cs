using HealthCare.Descriptions.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Domain.Entities
{
    public class HospitalPoliclinicQuota : IEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }


        // HospitalPoliclinicQuota Property

        public Guid HospitalPoliclinicId { get; set; }
        public Guid QuotaTypeId { get; set; }
        public int Quota { get; set; }
        public DateTimeOffset ValidityDate { get; set; }


        // Relations

        [ForeignKey(nameof(HospitalPoliclinicId))]
        public HospitalPoliclinic HospitalPoliclinic { get; set; }

        [ForeignKey(nameof(QuotaTypeId))]
        public QuotaType QuotaType { get; set; }
    }
}