using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Queries
{
    public class GetHospitalPoliclinicQuotasByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetHospitalPoliclinicQuotasByFilterQueryResult>>>,
        IPagedQueryBase
    {
        public string? HospitalCode { get; set; }
        public Guid? HospitalPoliclinicId { get; set; }
        public Guid? QuotaTypeId { get; set; }
        public DateTimeOffset? ValidityDate { get; set; }
        public bool? IsAvailable { get; set; }
        public string? Token { get; set; }

        [JsonConstructor]
        private GetHospitalPoliclinicQuotasByFilterQuery()
        {

        }

        public static GetHospitalPoliclinicQuotasByFilterQuery Filter(string? hospitalCode, Guid? hospitalPoliclinicId, Guid? quotaTypeId, DateTimeOffset? validityDate, bool? isAvailable) =>
            new GetHospitalPoliclinicQuotasByFilterQuery
            {
                HospitalCode = hospitalCode,
                HospitalPoliclinicId = hospitalPoliclinicId,
                QuotaTypeId = quotaTypeId,
                ValidityDate = validityDate,
                IsAvailable = isAvailable
            };
    }
}