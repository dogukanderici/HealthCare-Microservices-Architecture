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
    public class HospitalPoliclinic : IEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }


        // HospitalPoliclinic Property

        public Guid HospitalId { get; set; }
        public Guid PoliclinicId { get; set; }


        // Relations

        [ForeignKey(nameof(HospitalId))]
        public Hospital Hospital { get; set; }

        [ForeignKey(nameof(PoliclinicId))]
        public Policlinic Policlinic { get; set; }

        public List<HospitalPoliclinicQuota> HospitalPoliclinicQuotas { get; set; }
    }
}