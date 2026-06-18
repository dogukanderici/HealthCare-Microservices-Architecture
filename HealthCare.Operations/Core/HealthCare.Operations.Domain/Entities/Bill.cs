using HealthCare.Operations.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Domain.Entities
{
    public class Bill : GenericAuditProperty
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public string BillingNumber { get; set; }
        public DateTimeOffset BillingDate { get; set; }
        public string PatientNameSurname { get; set; }
        public string PatientPhone { get; set; }
        public bool PatientNationality { get; set; }
        public string PatientIDNumber { get; set; }
        public string PatientMail { get; set; }
        public string PatientAddress { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
