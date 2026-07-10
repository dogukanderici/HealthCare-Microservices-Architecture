using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries
{
    public class GetInstanceByFilterQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetInstancesByFilterQueryResult>>>
    {
        public int? Number { get; set; }
        public Guid? InitiatorWorkItemId { get; set; }
        public int? Status { get; set; }


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