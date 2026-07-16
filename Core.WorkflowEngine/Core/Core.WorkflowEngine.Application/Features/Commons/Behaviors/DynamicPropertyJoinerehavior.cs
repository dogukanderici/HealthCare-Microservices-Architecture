using Core.WorkflowEngine.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Commons.Behaviors
{
    public class DynamicPropertyJoinerehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IDynamicPropertyJoiner
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
