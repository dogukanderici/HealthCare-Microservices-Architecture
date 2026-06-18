using HealthCare.Descriptions.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Domain.Entities
{
    public class Policlinic : GenericAuditProperty
    {
        [Key]
        public Guid Id { get; set; }
        public int PoliclinicCode { get; set; }
        public string PoliclinicName { get; set; }
        public bool IsAvailable { get; set; }

        public List<HospitalPoliclinic> HospitalPoliclinics { get; set; }
    }
}
