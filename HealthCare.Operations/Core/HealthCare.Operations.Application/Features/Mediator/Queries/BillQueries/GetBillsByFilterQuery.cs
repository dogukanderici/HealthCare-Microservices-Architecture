using HealthCare.Operations.Application.Features.Mediator.Results.BillResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Queries.BillQueries
{
    public class GetBillsByFilterQuery : IRequest<List<GetBillsByFilterQueryResult>>
    {
        public Guid? AppointmentId { get; set; }
        public string? BillingNumber { get; set; }
        public DateTimeOffset? BillingDate { get; set; }
        public string? PatientNameSurname { get; set; }
        public string? PatientPhone { get; set; }
        public bool? PatientNationality { get; set; }
        public string? PatientIDNumber { get; set; }
        public string? PatientMail { get; set; }

        private GetBillsByFilterQuery() { }

        public static GetBillsByFilterQuery Filter(Guid? appointmentId,
            string? billingNumber,
            DateTimeOffset? billingDate,
            string? patientNameSurname,
            string? patientPhone,
            bool? patientNationality,
            string? patientIDNumber,
            string? patientMail) => new GetBillsByFilterQuery
            {
                AppointmentId = appointmentId,
                BillingNumber = billingNumber,
                BillingDate = billingDate,
                PatientNameSurname = patientNameSurname,
                PatientPhone = patientPhone,
                PatientNationality = patientNationality,
                PatientIDNumber = patientIDNumber,
                PatientMail = patientMail
            };
    }
}
