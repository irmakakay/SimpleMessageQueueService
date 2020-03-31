namespace MessageQueue.Common.Mapping
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;

    public class MappingContext : IMappingContext
    {
        private readonly IConfigurationProvider _configurationProvider;

        public MappingContext(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public TDestination MapFromMessageData<TSource, TDestination>(TSource source) 
            where TSource : IMessageData =>
            Mapper.Map<TDestination>(source);

        public TDestination Map<TSource, TDestination>(TSource source) 
            where TSource : IMapperSource 
            where TDestination : IMapperDestination =>
            Mapper.Map<TDestination>(source);

        public IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source) 
            where TSource : IMapperSource 
            where TDestination : IMapperDestination =>
            Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);

        public IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source, Action<IMappingOperationOptions> opts) 
            where TSource : IMapperSource 
            where TDestination : IMapperDestination =>
            Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source, opts);

        private IMapper Mapper => _configurationProvider.CreateMapper();
    }
}