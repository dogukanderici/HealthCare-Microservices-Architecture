using HealthCare.Descriptions.Application.Features.Mediator.Results.ServiceResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.ServiceQueries
{
    public class GetServiceTypesByFilterQuery : IRequest<List<GetServiceTypesByFilterQueryResult>>
    {
        public string? ServiceCode { get; set; }
        public string? ServiceName { get; set; }
        public bool? IsAvailable { get; set; }

        private GetServiceTypesByFilterQuery() { }

        public static GetServiceTypesByFilterQuery FromServiceCode(string serviceCode) => new GetServiceTypesByFilterQuery
        {
            ServiceCode = serviceCode,
        };

        public static GetServiceTypesByFilterQuery FromServiceName(string serviceName) => new GetServiceTypesByFilterQuery
        {
            ServiceName = serviceName,
        };

        public static GetServiceTypesByFilterQuery FullFilter(string? serviceCode, string? serviceName, bool? isAvailable) => new GetServiceTypesByFilterQuery
        {
            ServiceCode = serviceCode,
            ServiceName = serviceName,
            IsAvailable = isAvailable
        };
    }
}
