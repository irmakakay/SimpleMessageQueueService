namespace MessageQueue.Common
{
    using SimpleInjector;

    public static class CommonBootstrapper
    {
        public static Container Configure(Container container)
        {
            container.Register<IMessageSerializer, MessageSerializer>(Lifestyle.Singleton);
            container.Register<IDateTimeProvider, SystemDateTimeProvider>(Lifestyle.Singleton);

            return container;
        }
    }
}
