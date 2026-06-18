using HealthCare.Operations.Application.Features.Mediator.Commands.BillingServiceCommands;
using HealthCare.Operations.Application.Features.Mediator.Commands.GenericCommands;
using HealthCare.Operations.Application.Features.Mediator.Handlers.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Commands.BillCommands
{
    public class CreateBillWithBillingServiceCommand : GenericAuditCommand, IRequest<GenericHandlerResponse>
    {
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
        public List<CreateBillingServicesForBillDto> BillingServicesData { get; set; }
    }

    public class CreateBillingServicesForBillDto
    {
        public Guid BillingId { get; set; }
        public Guid ServiceId { get; set; }
        public int Piece { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
