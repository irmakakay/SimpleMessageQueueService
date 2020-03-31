namespace MessageQueue.Common.Mapping
{
    using System.Collections.Generic;
    using AutoMapper;

    public interface IMappingContext
  {
    /// <summary>
    /// Create Mapping between source and IMessageData
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    TDestination MapFromMessageData<TSource, TDestination>(TSource source) where TSource : IMessageData;

    /// <summary>
    /// Create Mapping from source to destination
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    TDestination Map<TSource, TDestination>(TSource source)
      where TSource : IMapperSource where TDestination : IMapperTarget;

    /// <summary>
    /// Create Mapping from source to destination list
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
      where TSource : IMapperSource where TDestination : IMapperTarget;

    /// <summary>
    /// Create Mapping from source to destination list
    /// </summary>
    /// <param name="source"></param>
    /// <param name="opts"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <returns></returns>
    IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source,
      System.Action<IMappingOperationOptions> opts) where TSource : IMapperSource where TDestination : IMapperTarget;
  }
}