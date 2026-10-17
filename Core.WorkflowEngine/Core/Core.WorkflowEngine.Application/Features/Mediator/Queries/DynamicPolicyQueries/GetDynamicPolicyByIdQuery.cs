using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicPolicyResults;
using Core.WorkflowEngine.Application.Features.Mediator.Wrappers.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.DynamicPolicyQueries
{
    public class GetDynamicPolicyByIdQuery : IRequest<InternalHandlerResponse<GetDynamicPolicyByIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetDynamicPolicyByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}