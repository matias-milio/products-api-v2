using System;
using System.Threading.Tasks;

namespace Products.Infrastructure.Intefaces
{
    public interface ICacheManager
    {
        Task<T> GetOrSet<T>(string cacheKey, Func<Task<T>> getItemCallback) where T : class;
    }
}
