namespace MessageQueue.Service
{
    using MessageQueue.Service.Services;
    using MessageQueue.Service.Validators;
    using SimpleInjector;

    public static class ServiceBootstrapper
    {
        public static Container Configure(Container container)
        {
            container.Register(typeof(IQueueMessageValidator<>), typeof(IQueueMessageValidator<>).Assembly);
            container.Register<IServiceVersionQueueService, ServiceVersionQueueService>(Lifestyle.Singleton);

            return container;
        }
    }
}