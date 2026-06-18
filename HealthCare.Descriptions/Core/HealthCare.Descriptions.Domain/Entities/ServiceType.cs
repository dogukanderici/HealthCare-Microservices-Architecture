using HealthCare.Descriptions.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Domain.Entities
{
    public class ServiceType : GenericAuditProperty
    {
        [Key]
        public Guid Id { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public bool IsAvailable { get; set; }
    }
}
