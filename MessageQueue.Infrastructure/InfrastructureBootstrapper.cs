namespace MessageQueue.Infrastructure
{
    using MessageQueue.Common;
    using MessageQueue.Infrastructure.Cache;
    using MessageQueue.Infrastructure.Factories;
    using MessageQueue.Infrastructure.Providers;
    using SimpleInjector;

    public static class InfrastructureBootstrapper
    {
        public static Container Configure(Container container)
        {
            container.Register<IRedisConnectionFactory, RedisConnectionFactory>(Lifestyle.Singleton);
            container.Register<IRedisCacheStore, RedisCacheStore>(Lifestyle.Singleton);
            container.Register<IDateTimeProvider, SystemDateTimeProvider>(Lifestyle.Singleton);
            container.Register<IQueueMessageFactory, QueueMessageFactory>(Lifestyle.Singleton);
            container.Register<IMessageQueueProvider, MessageQueueProvider>(Lifestyle.Singleton);

            return container;
        }
    }
}
