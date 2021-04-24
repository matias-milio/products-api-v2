using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Products.Helpers.ConfigModels;
using Products.Infrastructure.Intefaces;
using System;
using System.Threading.Tasks;

namespace Products.Infrastructure.Implementations
{
    public class CacheManager : ICacheManager
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _cacheSettings;

        public CacheManager(IDistributedCache cache, IOptions<CacheSettings> cacheSettings)
        {
            _cache = cache;
            _cacheSettings = cacheSettings.Value;
        }

        public async Task<T> GetOrSet<T>(string cacheKey, Func<Task<T>> getItemCallback) where T : class
        {
            var cachedJson = await _cache.GetStringAsync(cacheKey);
            if (string.IsNullOrWhiteSpace(cachedJson))
            {
                T item = await getItemCallback();
                var response = JsonConvert.SerializeObject(item);
                await _cache.SetStringAsync(cacheKey,
                                            response,
                                            new DistributedCacheEntryOptions{AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheSettings.ExpirationInMinutes) });
                return item;
            }
            return JsonConvert.DeserializeObject<T>(cachedJson);
        }

        public async Task ClearCache(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
