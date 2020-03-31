namespace MessageQueue.Service.Services
{
    using MessageQueue.Common.Configuration;

    public interface IConfigurationService
    {
        IQueueServiceConfiguration GetServiceConfiguration();
    }
}