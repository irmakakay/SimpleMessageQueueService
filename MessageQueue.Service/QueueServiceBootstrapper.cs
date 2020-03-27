namespace MessageQueue.Service
{
    using MessageQueue.Service.Services;
    using MessageQueue.Service.Validators;
    using SimpleInjector;

    public static class QueueServiceBootstrapper
    {
        public static void Configure(Container container)
        {
            container.Register(typeof(IQueueMessageValidator<>), typeof(IQueueMessageValidator<>).Assembly);
            container.Register<IConfigurationService, ConfigurationService>(Lifestyle.Singleton);
            container.Register<IServiceVersionQueueService, ServiceVersionQueueService>(Lifestyle.Singleton);
        }
    }
}