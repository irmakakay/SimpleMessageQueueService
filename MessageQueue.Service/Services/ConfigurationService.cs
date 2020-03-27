namespace MessageQueue.Service.Services
{
    using System;
    using MessageQueue.Service.Configuration;

    public interface IConfigurationService
    {
        IQueueServiceConfiguration GetServiceConfiguration();
    }

    public class ConfigurationService : IConfigurationService
    {
        public IQueueServiceConfiguration GetServiceConfiguration() =>
            throw new NotImplementedException();
    }
}
