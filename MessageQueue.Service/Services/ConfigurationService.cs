using System;
using System.Collections.Generic;
using System.Text;

namespace MessageQueue.Service.Services
{
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
