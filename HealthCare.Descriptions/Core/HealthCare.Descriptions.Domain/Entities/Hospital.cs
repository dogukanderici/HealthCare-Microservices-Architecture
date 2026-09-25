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
    public class Hospital : IEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }


        // Hospital Property

        public string HospitalCode { get; set; }
        public string HospitalName { get; set; }
        public Guid HospitalCity { get; set; }
        public Guid HospitalDistrict { get; set; }


        // Relations

        [ForeignKey(nameof(HospitalCity))]
        public City City { get; set; }

        [ForeignKey(nameof(HospitalDistrict))]
        public District District { get; set; }

        public List<HospitalPoliclinic> HospitalPoliclinic { get; set; }
        public HospitalService HospitalService { get; set; }
    }
}