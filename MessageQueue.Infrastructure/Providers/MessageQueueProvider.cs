namespace MessageQueue.Infrastructure.Providers
{
    using MessageQueue.Common;
    using MessageQueue.Infrastructure.Cache;
    using MessageQueue.Infrastructure.Factories;
    using Microsoft.Extensions.Logging;

    public class MessageQueueProvider : IMessageQueueProvider
    {
        private readonly IRedisCacheStore _redisCache;
        private readonly IQueueMessageFactory _messageFactory;
        private readonly ILoggerFactory _loggerFactory;

        public MessageQueueProvider(IRedisCacheStore redisCache, IQueueMessageFactory messageFactory, ILoggerFactory loggerFactory)
        {
            _redisCache = redisCache;
            _messageFactory = messageFactory;
            _loggerFactory = loggerFactory;
        }

        public IQueue<TData> GetQueue<TData>() where TData : IMessageData => 
            new RedisQueue<TData>(_redisCache, _messageFactory, _loggerFactory.CreateLogger<RedisQueue<TData>>());
    }
}
