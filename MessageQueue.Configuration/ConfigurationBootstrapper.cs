namespace MessageQueue.Configuration
{
    using MessageQueue.Configuration.Services;
    using Microsoft.Extensions.DependencyInjection;
    using SimpleInjector;

    public static class ConfigurationBootstrapper
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationService, ConfigurationService>();
        }

        public static Container Configure(Container container)
        {
            container.Register<IConfigurationService, ConfigurationService>(Lifestyle.Singleton);

            return container;
        }
    }
}
