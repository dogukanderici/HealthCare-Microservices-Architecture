using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Wrappers;
using Core.WorkflowEngine.Application.Features.Wrappers.Responses;
using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessDefinitionQueries
{
    public class GetProcessDefinitionsQuery : IRequest<InternalHandlerResponse<IReadOnlyCollection<GetProcessDefinitionsQueryResult>>>, ICacheableQuery
    {
        public string CacheKey => CacheKeyGenerator.GenerateCacheKey(typeof(GetProcessDefinitionsQuery).Name);

        public TimeSpan ExpirationTime => TimeSpan.FromHours(1);
    }
}
