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
    public class Policlinic : IEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public Guid CreatedBy { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
        public Guid UpdatedBy { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");


        // Policlinic Property

        public int PoliclinicCode { get; set; }
        public string PoliclinicName { get; set; }
        public List<HospitalPoliclinic> HospitalPoliclinic { get; set; }
    }
}