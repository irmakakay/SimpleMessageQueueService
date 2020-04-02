﻿namespace MessageQueue.Common
{
    using AutoMapper;
    using MessageQueue.Common.Mapping;
    using MessageQueue.Common.Model;
    using MessageQueue.Configuration.Services;
    using Microsoft.Extensions.DependencyInjection;
    using SimpleInjector;

    public static class CommonBootstrapper
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddSingleton<IMessageSerializer, MessageSerializer>();
            services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
            services.AddSingleton<IMappingContext, MappingContext>();

            services.AddTransient<IConfigurationProvider>(
                _ => new MapperConfiguration(cfg => cfg.CreateMap<ServiceVersionRequest, ServiceVersionResponse>()));
        }

        public static Container Configure(Container container)
        {
            container.Register<IConfigurationService, ConfigurationService>(Lifestyle.Singleton);
            container.Register<IMessageSerializer, MessageSerializer>(Lifestyle.Singleton);
            container.Register<IDateTimeProvider, SystemDateTimeProvider>(Lifestyle.Singleton);
            container.Register<IMappingContext, MappingContext>(Lifestyle.Singleton);

            ConfigureMapperConfiguration(container);

            return container;
        }

        public static Container ConfigureMapperConfiguration(Container container)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ServiceVersionRequest, ServiceVersionResponse>();
            });

            container.RegisterInstance(typeof(IConfigurationProvider), config);

            return container;
        }
    }
}
