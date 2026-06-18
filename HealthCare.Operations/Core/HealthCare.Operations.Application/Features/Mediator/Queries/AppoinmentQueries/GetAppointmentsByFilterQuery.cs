using HealthCare.Operations.Application.Features.Mediator.Results.AppoinmentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Queries.AppoinmentQueries
{
    public class GetAppointmentsByFilterQuery : IRequest<List<GetAppointmentsByFilterQueryResult>>
    {
        public string? NameSurname { get; set; }
        public bool? Nationality { get; set; }
        public string? IDNumber { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public Guid? HospitalId { get; set; }
        public Guid? PoliclinicId { get; set; }
        public int? City { get; set; }
        public Guid? District { get; set; }
        public DateTimeOffset? AppointmentDate { get; set; }
        public bool? IsClosed { get; set; }

        private GetAppointmentsByFilterQuery() { }

        public static GetAppointmentsByFilterQuery Filter(
            string? nameSurname,
            bool? nationality,
            string? idNumber,
            string? phone,
            string? email,
            Guid? hospitalId,
            Guid? policlinicId,
            int? city,
            Guid? district,
            DateTimeOffset? appointmentDate,
            bool? ısClosed) => new GetAppointmentsByFilterQuery
            {
                NameSurname = nameSurname,
                Nationality = nationality,
                IDNumber = idNumber,
                Phone = phone,
                Email = email,
                HospitalId = hospitalId,
                PoliclinicId = policlinicId,
                City = city,
                District = district,
                AppointmentDate = appointmentDate,
                IsClosed = ısClosed
            };

    }
}
