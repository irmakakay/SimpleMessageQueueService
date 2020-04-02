namespace MessageQueue.Configuration
{
    using MessageQueue.Configuration.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class ConfigurationBootstrapper
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationService, ConfigurationService>();
        }
    }
}
