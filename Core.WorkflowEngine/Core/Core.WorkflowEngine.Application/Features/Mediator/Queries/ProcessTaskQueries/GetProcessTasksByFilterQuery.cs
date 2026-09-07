using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System.Text.Json.Serialization;

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

        // Unit Test veya Backend'de başka bir yerde kullanmak istenirse factory metot kullanılabilir.

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