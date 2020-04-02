namespace MessageQueue.Configuration.Services
{
    using MessageQueue.Configuration.Sections;

    public class ConfigurationService : IConfigurationService
    {
        public IQueueServiceConfiguration GetServiceConfiguration() =>
            new QueueServiceConfiguration {ProducerEnabled = true};

        public IRedisCacheConfiguration GetRedisConfiguration() =>
            new RedisCacheConfiguration
            {
                ConnectionString = @"localhost:6379,abortConnect=false,connectTimeout=25"
            };
    }
}
