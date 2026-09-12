using HealthCare.Descriptions.Application.Features.Mediators.Districts.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Districts.Queries
{
    public class GetDistrictsByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetDistrictsByFilterQueryResult>>>, IValidationRequest
    {
        public int? Plate { get; set; }

        [JsonConstructor]
        private GetDistrictsByFilterQuery()
        {

        }

        public static GetDistrictsByFilterQuery Filter(int? plate) =>
             new GetDistrictsByFilterQuery
             {
                 Plate = plate
             };
    }
}
