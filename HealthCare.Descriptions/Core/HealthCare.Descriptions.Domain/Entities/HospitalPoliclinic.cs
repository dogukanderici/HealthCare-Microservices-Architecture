using HealthCare.Descriptions.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Domain.Entities
{
    public class HospitalPoliclinic : GenericAuditProperty
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HospitalId { get; set; }
        public Guid PoliclinicId { get; set; }
        public bool IsAvailable { get; set; }

        public Hospital Hospital { get; set; }
        public Policlinic Policlinic { get; set; }
        public List<HospitalPoliclinicQuota> HospitalPoliclinicQuotas { get; set; }
    }
}
