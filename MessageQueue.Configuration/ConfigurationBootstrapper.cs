namespace MessageQueue.Configuration
{
    using MessageQueue.Configuration.Services;
    using SimpleInjector;

    public static class ConfigurationBootstrapper
    {
        public static Container Configure(Container container)
        {
            container.Register<IConfigurationService, ConfigurationService>(Lifestyle.Singleton);

            return container;
        }
    }
}
