using System.Collections.Generic;

namespace MessageQueue.Infrastructure.Cache
{
    using System.Threading.Tasks;

    public interface IRedisCacheStore
    {
        Task<long> ListRightPushAsync<T>(string key, T value);

        Task<T> ListLeftPopAsync<T>(string key);

        Task<IEnumerable<T>> GetRangeAsync<T>(string key, long start = 0, long stop = -1);

        Task<long> ListLengthAsync(string key);
    }
}
