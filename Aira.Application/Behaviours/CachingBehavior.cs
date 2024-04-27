namespace Aira.Application.Behaviours;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableMediatrQuery
{
    private readonly IDistributedCache _cache;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public CachingBehavior(IDistributedCache cache)
    {
        _cache = cache;
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not ICacheableMediatrQuery cacheableQuery) return await next();
        TResponse response;
        if (cacheableQuery.BypassCache) return await next();

        var cachedResponse = await _cache.GetAsync(cacheableQuery.CacheKey, cancellationToken);
        if (cachedResponse != null)
        {
            response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
        }
        else
        {
            response = await GetResponseAndAddToCache();
        }

        return response;

        async Task<TResponse> GetResponseAndAddToCache()
        {
            response = await next();

            try
            {
                await _semaphore.WaitAsync(cancellationToken);
                var slidingExpiration = cacheableQuery.SlidingExpiration;
                var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };
                var serializedData = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));
                await _cache.SetAsync(cacheableQuery.CacheKey, serializedData, options, cancellationToken);
            }
            finally
            {
                _semaphore.Release();
            }

            return response;
        }
    }
}