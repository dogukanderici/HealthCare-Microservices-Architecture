using HealthCare.Descriptions.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Domain.Entities
{
    public class District : GenericAuditProperty
    {
        [Key]
        public Guid Id { get; set; }
        public int Plate { get; set; }
        public string DistrictName { get; set; }
        public bool IsAvailable { get; set; }

        public City City { get; set; }
        public List<Hospital> Hospitals { get; set; }
    }
}
