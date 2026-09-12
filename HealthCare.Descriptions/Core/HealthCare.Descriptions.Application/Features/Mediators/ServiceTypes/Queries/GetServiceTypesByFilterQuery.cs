using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Queries
{
    public class GetServiceTypesByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetServiceTypesByFilterQueryResult>>>, IValidationRequest
    {
        public string? ServiceCode { get; set; }
        public string? ServiceName { get; set; }

        [JsonConstructor]
        private GetServiceTypesByFilterQuery()
        {

        }

        public static GetServiceTypesByFilterQuery Filter(string? serviceCode, string? serviceName) =>
            new GetServiceTypesByFilterQuery
            {
                ServiceCode = serviceCode,
                ServiceName = serviceName
            };
    }
}
