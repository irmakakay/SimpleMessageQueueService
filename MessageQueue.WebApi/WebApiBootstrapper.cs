namespace MessageQueue.WebApi
{
    using MessageQueue.Common;
    using MessageQueue.Infrastructure;
    using MessageQueue.Service;
    using Microsoft.Extensions.DependencyInjection;
    using SimpleInjector;

    public static class WebApiBootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            CommonBootstrapper.Configure(services);
            InfrastructureBootstrapper.Configure(services);
            ServiceBootstrapper.Configure(services);
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
