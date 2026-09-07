using Core.WorkflowEngine.Application.Interfaces;
using MediatR;

namespace Core.WorkflowEngine.Application.Behaviors
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
