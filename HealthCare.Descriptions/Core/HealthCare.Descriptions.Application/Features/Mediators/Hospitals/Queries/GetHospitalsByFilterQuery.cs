using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Queries
{
    public class GetHospitalsByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetHospitalsByFilterQueryResult>>>
    {
        public Guid? CityId { get; set; }
        public Guid? DistrictId { get; set; }
        public string? Code { get; set; }
        public bool? IsActive { get; set; }

        [JsonConstructor]
        private GetHospitalsByFilterQuery()
        {

        }

        public static GetHospitalsByFilterQuery Filter(Guid? cityId, Guid? districtId, string? code, bool? isActive) =>
            new GetHospitalsByFilterQuery
            {
                CityId = cityId,
                DistrictId = districtId,
                Code = code,
                IsActive = isActive
            };
    }
}
