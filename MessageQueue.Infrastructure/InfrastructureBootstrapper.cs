namespace MessageQueue.Infrastructure
{
    using MessageQueue.Common;
    using MessageQueue.Infrastructure.Cache;
    using MessageQueue.Infrastructure.Factories;
    using MessageQueue.Infrastructure.Providers;
    using Microsoft.Extensions.DependencyInjection;
    using SimpleInjector;

    public static class InfrastructureBootstrapper
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IRedisConnectionFactory, RedisConnectionFactory>();
            services.AddSingleton<IRedisCacheStore, RedisCacheStore>();
            services.AddSingleton<IQueueMessageFactory, QueueMessageFactory>();
            services.AddSingleton<IMessageQueueProvider, MessageQueueProvider>();
        }

        public static Container Configure(Container container)
        {
            container.Register<IRedisConnectionFactory, RedisConnectionFactory>(Lifestyle.Singleton);
            container.Register<IRedisCacheStore, RedisCacheStore>(Lifestyle.Singleton);
            container.Register<IQueueMessageFactory, QueueMessageFactory>(Lifestyle.Singleton);
            container.Register<IMessageQueueProvider, MessageQueueProvider>(Lifestyle.Singleton);

            return container;
        }
    }
}
