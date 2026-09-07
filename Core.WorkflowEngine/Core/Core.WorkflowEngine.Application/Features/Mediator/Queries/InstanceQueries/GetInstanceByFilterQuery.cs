using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System.Text.Json.Serialization;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries
{
    public class GetInstanceByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetInstancesByFilterQueryResult>>>, ICacheableQuery
    {
        public int? Number { get; set; }
        public Guid? InitiatorWorkItemId { get; set; }
        public int? Status { get; set; }

        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(
            [
                typeof(GetInstanceByFilterQuery).Name,
                Number.HasValue ? Number.ToString() : "1",
                InitiatorWorkItemId.HasValue ? InitiatorWorkItemId.ToString() : Guid.Empty.ToString(),
                Status.HasValue ? Status.ToString() : "1"
            ]
        );

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);


        // Oluşturlan Nesnenin Tek Başına Çağrılmasını Engeller. Static Metotu Kullanıma Zorlar.
        [JsonConstructor]
        private GetInstanceByFilterQuery()
        {

        }

        public static GetInstanceByFilterQuery Filter(int? number, Guid? initiatorWorkItemId, int? status) =>
            new GetInstanceByFilterQuery
            {
                Number = number,
                InitiatorWorkItemId = initiatorWorkItemId,
                Status = status
            };
    }
}