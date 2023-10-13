using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace jwt.DistributedCachExtension
{
    public static class DistributedCacheExtension
    {
        public static async Task<T?> GetOrCreateCache<T>(this IDistributedCache _distributedCache,string Key, Func<Task<T>>factory)
        {
            var cachedValue = await _distributedCache.GetStringAsync(Key);
            T result;
            if (cachedValue is not null) {

                result = Desrialize<T>(cachedValue);
            }

            else
            {
                result = await factory();

                if (result is not null)
                {
                    var tocached = Serialize(result);
                    await _distributedCache.SetStringAsync(Key, tocached);
                }

            }

            return result;
        }


        private static T Desrialize<T>(string cached)
        {
            var result= JsonConvert.DeserializeObject<T>(cached);
            return result;
        }
        private static string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
