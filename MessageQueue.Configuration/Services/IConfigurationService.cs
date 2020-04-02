namespace MessageQueue.Configuration.Services
{
    using MessageQueue.Configuration.Sections;

    public interface IConfigurationService
    {
        IQueueServiceConfiguration GetServiceConfiguration();

        IRedisCacheConfiguration GetRedisConfiguration();
    }
}