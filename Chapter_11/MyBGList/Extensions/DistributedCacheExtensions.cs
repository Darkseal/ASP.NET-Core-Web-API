using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace MyBGList.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static bool TryGetValue<T>(
            this IDistributedCache cache,
            string key,
            out T? value)
        {
            value = default;
            var val = cache.Get(key);
            if (val == null) return false;
            value = JsonSerializer.Deserialize<T>(val);
            return true;
        }

        public static void Set<T>(
            this IDistributedCache cache,
            string key,
            T value,
            TimeSpan absoluteExpirationRelativeToNow)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value));
            cache.Set(key, bytes, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow
            });
        }
    }
}
