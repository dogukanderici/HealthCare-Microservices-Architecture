using HealthCare.Descriptions.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Domain.Entities
{
    public class AppointmentStatus : GenericAuditProperty
    {
        [Key]
        public Guid Id { get; set; }
        public string StatusName { get; set; }
        public bool IsAvailable { get; set; }
    }
}
