using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicQuotaResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalPoliclinicQuotaQueries
{
    public class GetHospitalPoliclinicQuotasByFilterQuery : IRequest<List<GetHospitalPoliclinicQuotasByFilterQueryResult>>
    {
        public Guid? QuotaType { get; set; }
        public DateTimeOffset? ValidityDate { get; set; }
        public bool? IsAvailable { get; set; }

        private GetHospitalPoliclinicQuotasByFilterQuery() { }

        public static GetHospitalPoliclinicQuotasByFilterQuery Filter(Guid? quotaType, DateTimeOffset? validityDate, bool? isAvailable) =>
            new GetHospitalPoliclinicQuotasByFilterQuery
            {
                QuotaType = quotaType,
                ValidityDate = validityDate,
                IsAvailable = isAvailable
            };
    }
}
