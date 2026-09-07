using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using MediatR;

namespace Core.WorkflowEngine.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheableQuery, IRequest<TResponse>
        where TResponse : IInternalCommandResponse
    {
        private readonly ICacheProvider _cacheProvider;

        public CachingBehavior(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            string cacheKey = request.CacheKey;

            bool isCachedDataExists = await _cacheProvider.IsKeyExistsAsync(cacheKey);

            if (isCachedDataExists)
            {
                var cacheResult = await _cacheProvider.GetCacheDataAsync<TResponse>(cacheKey);

                return cacheResult;
            }

            TResponse response = await next();

            bool cacheSetResult = await _cacheProvider.SetCacheDataAsync(cacheKey, response, request.ExpirationTime);

            return response;
        }
    }
}