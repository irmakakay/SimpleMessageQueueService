namespace MessageQueue.Infrastructure.Cache
{
    using System;
    using System.Threading.Tasks;
    using MessageQueue.Logging;
    using NeoSmart.AsyncLock;
    using StackExchange.Redis;

    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly IRedisCacheConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly AsyncLock _lock = new AsyncLock();

        private volatile ConnectionMultiplexer _connection;

        public RedisConnectionFactory(IRedisCacheConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IDatabase> GetDatabaseAsync() =>
            (await GetRedisAsync().ConfigureAwait(false)).GetDatabase();

        private async Task<ConnectionMultiplexer> GetRedisAsync()
        {
            if (_connection != null && _connection.IsConnected) return _connection;
            using (await _lock.LockAsync().ConfigureAwait(false))
            {
                return _connection = await ConnectToRedisAsync().ConfigureAwait(false);
            }
        }

        public async Task<ConnectionMultiplexer> ConnectToRedisAsync()
        {
            try
            {
                var connection = await ConnectionMultiplexer.ConnectAsync(_configuration.ConnectionString)
                                     .ConfigureAwait(false);

                connection.PreserveAsyncOrder = false;

                _logger.Debug("New Redis ConnectionMultiplexer created");

                return connection;
            }
            catch (Exception ex)
            {
                _logger.Error("There was a problem connecting to Redis", ex);
                throw;
            }
        }
    }
}
