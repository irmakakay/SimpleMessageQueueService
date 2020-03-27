namespace MessageQueue.Infrastructure.Providers
{
    using MessageQueue.Common;
    using MessageQueue.Infrastructure.Cache;
    using MessageQueue.Infrastructure.Factories;
    using MessageQueue.Logging;

    public class MessageQueueProvider : IMessageQueueProvider
    {
        private readonly IRedisCacheStore _redisCache;
        private readonly IQueueMessageFactory _messageFactory;
        private readonly ILogger _logger;

        public MessageQueueProvider(IRedisCacheStore redisCache, IQueueMessageFactory messageFactory, ILogger logger)
        {
            _redisCache = redisCache;
            _messageFactory = messageFactory;
            _logger = logger;
        }

        public IQueue<TData> GetQueue<TData>() where TData : IMessageData => new RedisQueue<TData>(_redisCache, _messageFactory, _logger);
    }
}
