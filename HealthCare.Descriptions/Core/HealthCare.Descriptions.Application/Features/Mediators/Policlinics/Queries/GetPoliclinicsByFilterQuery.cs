using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Queries
{
    public class GetPoliclinicsByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetPoliclinicsByFilterQueryResult>>>, IValidationRequest
    {
        public string? Code { get; set; }
        public bool? IsActive { get; set; }

        [JsonConstructor]
        private GetPoliclinicsByFilterQuery()
        {

        }

        public static GetPoliclinicsByFilterQuery Filter(string? code, bool? isActive) =>
            new GetPoliclinicsByFilterQuery
            {
                Code = code,
                IsActive = isActive
            };
    }
}