using HealthCare.Operations.Application.Features.Mediator.Commands.GenericCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Commands.AppoinmentCommands
{
    public class UpdateAppointmentCommand : GenericAuditCommand, IRequest
    {
        public Guid Id { get; set; }
        public string NameSurname { get; set; }
        public bool Nationality { get; set; }
        public string IDNumber { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string Phone { get; set; }
        public string SecondPhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Guid HospitalId { get; set; }
        public Guid PoliclinicId { get; set; }
        public int City { get; set; }
        public Guid District { get; set; }
        public DateTimeOffset AppointmentDate { get; set; }
        public bool IsClosed { get; set; }
    }
}
