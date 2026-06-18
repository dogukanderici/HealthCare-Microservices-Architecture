using HealthCare.Descriptions.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Domain.Entities
{
    public class Hospital : GenericAuditProperty
    {
        [Key]
        public Guid Id { get; set; }
        public int HospitalCode { get; set; }
        public string HospitalName { get; set; }
        public int HospitalCity { get; set; }
        public Guid HospitalDistrict { get; set; }
        public bool IsAvailable { get; set; }

        public List<HospitalPoliclinic> HospitalPoliclinics { get; set; }
        public City City { get; set; }
        public District District { get; set; }
    }
}
