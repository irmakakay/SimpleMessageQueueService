namespace MessageQueue.Infrastructure.Cache
{
    using System;
    using System.Threading.Tasks;
    using MessageQueue.Configuration.Sections;
    using MessageQueue.Infrastructure.Extensions;
    using Microsoft.Extensions.Logging;
    using NeoSmart.AsyncLock;
    using StackExchange.Redis;

    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly IRedisCacheConfiguration _configuration;
        private readonly ILogger<RedisConnectionFactory> _logger;
        private readonly AsyncLock _lock = new AsyncLock();
        private readonly Lazy<ConfigurationOptions> _lazyConfigurationOptions;

        private volatile ConnectionMultiplexer _connection;

        public RedisConnectionFactory(IRedisCacheConfiguration configuration, ILogger<RedisConnectionFactory> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _lazyConfigurationOptions = new Lazy<ConfigurationOptions>(GetConfigurationOptions);
        }

        public async Task<IDatabase> GetDatabaseAsync() =>
            (await GetRedisAsync().ConfigureAwait(false)).GetDatabase();

        private async Task<ConnectionMultiplexer> GetRedisAsync()
        {
            if (_connection.IsOpen()) return _connection;

            using (await _lock.LockAsync().ConfigureAwait(false))
            {
                return _connection = await ConnectToRedisAsync().ConfigureAwait(false);
            }
        }

        private async Task<ConnectionMultiplexer> ConnectToRedisAsync()
        {
            try
            {
                var connection = await ConnectionMultiplexer.ConnectAsync(_lazyConfigurationOptions.Value)
                                     .ConfigureAwait(false);

                _logger.LogDebug("New Redis ConnectionMultiplexer created");

                return connection;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was a problem connecting to Redis", ex);
                throw;
            }
        }

        private ConfigurationOptions GetConfigurationOptions() =>
            new ConfigurationOptions
            {
                EndPoints = { _configuration.ConnectionString },
                ConnectRetry = 3,
                ReconnectRetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(5).Milliseconds, TimeSpan.FromSeconds(20).Milliseconds),
                ConnectTimeout = 2000
            };
    }
}
