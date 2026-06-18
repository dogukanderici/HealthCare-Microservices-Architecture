using HealthCare.Operations.Application.Features.Mediator.Commands.GenericCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Commands.BillingServiceCommands
{
    public class CreateBillingServiceCommand : GenericAuditCommand, IRequest
    {
        // Bu class güncellenirse CreateBillingServicesForBillDto class'ı da güncellenmeli.
        public Guid BillingId { get; set; }
        public Guid ServiceId { get; set; }
        public int Piece { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
