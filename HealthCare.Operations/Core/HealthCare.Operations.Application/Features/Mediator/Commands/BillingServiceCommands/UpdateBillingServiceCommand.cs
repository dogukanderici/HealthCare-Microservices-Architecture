using HealthCare.Operations.Application.Features.Mediator.Commands.GenericCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Commands.BillingServiceCommands
{
    public class UpdateBillingServiceCommand : GenericAuditCommand, IRequest
    {
        public Guid Id { get; set; }
        public Guid BillingId { get; set; }
        public Guid ServiceId { get; set; }
        public int Piece { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
