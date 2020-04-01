namespace MessageQueue.Service
{
    using MessageQueue.Common.Model;
    using MessageQueue.Service.Services;
    using MessageQueue.Service.Validators;
    using Microsoft.Extensions.DependencyInjection;
    using SimpleInjector;

    public static class ServiceBootstrapper
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IServiceVersionQueueService, ServiceVersionQueueService>();
            services.AddSingleton<IQueueMessageValidator<ServiceVersionRequest>, ServiceVersionMessageValidator>();
        }

        public static Container Configure(Container container)
        {
            container.Register(typeof(IQueueMessageValidator<>), typeof(IQueueMessageValidator<>).Assembly);
            container.Register<IServiceVersionQueueService, ServiceVersionQueueService>(Lifestyle.Singleton);

            return container;
        }
    }
}