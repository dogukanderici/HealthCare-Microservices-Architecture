using HealthCare.Descriptions.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Domain.Entities
{
    public class HospitalPoliclinicQuota : GenericAuditProperty
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HospitalPoliclinicId { get; set; }
        public Guid QuotaType { get; set; }
        public int Quota { get; set; }
        public DateTimeOffset ValidityDate { get; set; }
        public bool IsAvailable { get; set; }

        public HospitalPoliclinic HospitalPoliclinic { get; set; }
    }
}
