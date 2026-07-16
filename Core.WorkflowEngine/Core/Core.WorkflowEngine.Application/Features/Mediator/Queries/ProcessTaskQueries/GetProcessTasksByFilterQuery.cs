using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Features.Wrappers;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskQueries
{
    public class GetProcessTasksByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetProcessTasksByFilterQueryResult>>>, ICacheableQuery
    {
        public Guid? ProcessId { get; set; }
        public string? StepName { get; set; }
        public bool? IsStartStep { get; set; }
        public bool? IsActive { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetProcessTasksByFilterQuery).Name,
                (ProcessId.ToString())
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);

        [JsonConstructor]
        private GetProcessTasksByFilterQuery()
        {

        }

        public static GetProcessTasksByFilterQuery Filter(Guid? processId, string? stepName, bool? isStartStep, bool? isActivce) =>
            new GetProcessTasksByFilterQuery
            {
                ProcessId = processId,
                StepName = stepName,
                IsStartStep = isStartStep,
                IsActive = isActivce
            };
    }
}