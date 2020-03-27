namespace MessageQueue.Infrastructure.Cache
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using StackExchange.Redis;

    public class RedisCacheStore : IRedisCacheStore
    {
        private readonly IRedisConnectionFactory _connectionFactory;
        private readonly ICacheSerializer _serializer;

        public RedisCacheStore(IRedisConnectionFactory connectionFactory, ICacheSerializer serializer)
        {
            _connectionFactory = connectionFactory;
            _serializer = serializer;
        }

        public async Task<long> ListRightPushAsync<T>(string key, T value)
        {
            var db = await GetDatabaseAsync().ConfigureAwait(false);

            var currentLength = await ListLengthAsync(key).ConfigureAwait(false);
            
            if (value == null) return currentLength;
            
            var serializedObject = _serializer.Serialize(value);
            
            if (serializedObject == null) return currentLength;

            return await db.ListRightPushAsync(key, serializedObject).ConfigureAwait(false);
        }

        public async Task<T> ListLeftPopAsync<T>(string key)
        {
            var db = await GetDatabaseAsync().ConfigureAwait(false);

            var value = await db.ListLeftPopAsync(key).ConfigureAwait(false);

            return value.HasValue ? _serializer.Deserialize<T>(value) : default(T);
        }

        public async Task<IEnumerable<T>> GetRangeAsync<T>(string key, long start = 0, long stop = -1)
        {
            var db = await GetDatabaseAsync().ConfigureAwait(false);

            return (await db.ListRangeAsync(key, start, stop).ConfigureAwait(false))
                .Where(_ => _ != RedisValue.Null)
                .Select(_ => _serializer.Deserialize<T>(_));
        }

        public async Task<long> ListLengthAsync(string key)
        {
            var db = await GetDatabaseAsync().ConfigureAwait(false);

            return await db.ListLengthAsync(key).ConfigureAwait(false);
        }

        private async Task<IDatabase> GetDatabaseAsync() => 
            await _connectionFactory.GetDatabaseAsync().ConfigureAwait(false);
    }
}
