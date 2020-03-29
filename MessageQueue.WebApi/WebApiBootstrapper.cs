namespace MessageQueue.WebApi
{
    using MessageQueue.Common;
    using MessageQueue.Infrastructure;
    using MessageQueue.Service;
    using SimpleInjector;

    public static class WebApiBootstrapper
    {
        public static Container Configure(Container container)
        {
            CommonBootstrapper.Configure(container);
            InfrastructureBootstrapper.Configure(container);
            ServiceBootstrapper.Configure(container);

            return container;
        }
    }
}
