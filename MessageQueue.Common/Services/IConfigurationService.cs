namespace MessageQueue.Common.Services
{
    using MessageQueue.Common.Configuration;

    public interface IConfigurationService
    {
        IQueueServiceConfiguration GetServiceConfiguration();
    }
}