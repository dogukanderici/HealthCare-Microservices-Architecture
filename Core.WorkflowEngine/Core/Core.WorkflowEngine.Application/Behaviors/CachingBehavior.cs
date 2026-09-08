using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.CacheServices;
using MediatR;

namespace Core.WorkflowEngine.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheableQuery, IRequest<TResponse>
        where TResponse : IInternalCommandResponse
    {
        private readonly ICacheQueryProvider _cacheQueryProvider;
        private readonly ICacheCommandProvider _cacheCommandProvider;

        public CachingBehavior(ICacheQueryProvider cacheQueryProvider, ICacheCommandProvider cacheCommandProvider)
        {
            _cacheQueryProvider = cacheQueryProvider;
            _cacheCommandProvider = cacheCommandProvider;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            string cacheKey = request.CacheKey;

            bool isCachedDataExists = await _cacheQueryProvider.IsKeyExistsAsync(cacheKey);

            if (isCachedDataExists)
            {
                var cacheResult = await _cacheQueryProvider.GetCacheDataAsync<TResponse>(cacheKey);

                return cacheResult;
            }

            TResponse response = await next();

            bool cacheSetResult = await _cacheCommandProvider.SetCacheDataAsync(cacheKey, response, request.ExpirationTime);

            return response;
        }
    }
}