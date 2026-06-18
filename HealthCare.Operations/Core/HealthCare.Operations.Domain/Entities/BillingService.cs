using HealthCare.Operations.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Domain.Entities
{
    public class BillingService : GenericAuditProperty
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BillingId { get; set; }
        public Guid ServiceId { get; set; }
        public int Piece { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
