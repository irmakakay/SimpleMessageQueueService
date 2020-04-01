namespace MessageQueue.Common.Services
{
    using MessageQueue.Common.Configuration;

    public class ConfigurationService : IConfigurationService
    {
        public IQueueServiceConfiguration GetServiceConfiguration() =>
            new QueueServiceConfiguration
            {
                ProducerEnabled = true
            };
    }
}
