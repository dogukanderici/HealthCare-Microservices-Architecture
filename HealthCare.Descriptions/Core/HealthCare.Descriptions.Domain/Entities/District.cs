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
    public class District : IEntity
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }


        // District Property

        public Guid CityId { get; set; }
        public int Plate { get; set; }
        public string DistrictName { get; set; }

        // Relations

        [ForeignKey(nameof(CityId))]
        public City City { get; set; }

        public List<Hospital> Hospitals { get; set; }
    }
}