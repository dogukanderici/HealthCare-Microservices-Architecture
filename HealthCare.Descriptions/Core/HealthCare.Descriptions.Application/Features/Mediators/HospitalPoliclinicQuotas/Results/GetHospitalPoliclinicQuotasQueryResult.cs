using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Results.Shared;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Results
{
    public class GetHospitalPoliclinicQuotasQueryResult : IListResult
    {
        public Guid Id { get; set; }
        public Guid HospitalPoliclinicId { get; set; }
        public Guid QuotaTypeId { get; set; }
        public int Quota { get; set; }
        public DateTimeOffset ValidityDate { get; set; }
        public bool IsAvailable { get; set; }

        public HospitalPoliclinicSharedResult HospitalPoliclinic { get; set; }
    }
}