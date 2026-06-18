using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.InstanceQueries
{
    public class GetInstanceByFilterQuery : IRequest<List<GetInstancesByFilterQueryResult>>
    {
        public int? Number { get; set; }
        public Guid? InitiatorWorkItemId { get; set; }
        public int? Status { get; set; }


        // Oluşturlan Nesnenin Tek Başına Çağrılmasını Engeller. Static Metotu Kullanıma Zorlar.
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