namespace MessageQueue.WebApi
{
    using AutoMapper;
    using MessageQueue.Common;
    using MessageQueue.Common.Mapping;
    using MessageQueue.Common.Model;
    using MessageQueue.Configuration.Services;
    using MessageQueue.Infrastructure;
    using MessageQueue.Infrastructure.Cache;
    using MessageQueue.Infrastructure.Factories;
    using MessageQueue.Infrastructure.Providers;
    using MessageQueue.Service;
    using MessageQueue.Service.Services;
    using MessageQueue.Service.Validators;
    using Microsoft.Extensions.DependencyInjection;
    using SimpleInjector;

    public static class WebApiBootstrapper
    {
        public static void Configure(IServiceCollection services)
        {
            RegisterConfigurationServices(services);
            RegisterCommonServices(services);
            RegisterInfrastructureServices(services);
            RegisterMainServices(services);
        }

        private static void RegisterConfigurationServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationService, ConfigurationService>();
        }

        private static void RegisterCommonServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddSingleton<IMessageSerializer, MessageSerializer>();
            services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
            services.AddSingleton<IMappingContext, MappingContext>();

            services.AddTransient<IConfigurationProvider>(
                _ => new MapperConfiguration(cfg => cfg.CreateMap<ServiceVersionRequest, ServiceVersionResponse>()));
        }

        private static void RegisterInfrastructureServices(IServiceCollection services)
        {
            services.AddSingleton<IRedisConnectionFactory, RedisConnectionFactory>();
            services.AddSingleton<IRedisCacheStore, RedisCacheStore>();
            services.AddSingleton<IQueueMessageFactory, QueueMessageFactory>();
            services.AddSingleton<IMessageQueueProvider, MessageQueueProvider>();
        }

        private static void RegisterMainServices(IServiceCollection services)
        {
            services.AddSingleton<IServiceVersionQueueService, ServiceVersionQueueService>();
            services.AddSingleton<IQueueMessageValidator<ServiceVersionRequest>, ServiceVersionMessageValidator>();
        }

        public static Container Configure(Container container)
        {
            CommonBootstrapper.Configure(container);
            InfrastructureBootstrapper.Configure(container);
            ServiceBootstrapper.Configure(container);

            return container;
        }
    }
}
