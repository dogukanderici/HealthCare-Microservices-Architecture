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
    public class QuotaType : IEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }


        // QuotaType Property

        public string QuotaTypeName { get; set; }
        public string QuotaTypeCode { get; set; }

        // Relations

        public List<HospitalPoliclinicQuota> HospitalPoliclinicQuotas { get; set; }
    }
}