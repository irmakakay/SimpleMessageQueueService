namespace MessageQueue.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MessageQueue.Common;
    using MessageQueue.Infrastructure.Cache;
    using MessageQueue.Infrastructure.Exceptions;
    using MessageQueue.Infrastructure.Factories;
    using MessageQueue.Infrastructure.Helpers;
    using Microsoft.Extensions.Logging;

    public class RedisQueue<TData> : IQueue<TData> where TData : IMessageData
    {
        private readonly IRedisCacheStore _redisCache;
        private readonly IQueueMessageFactory _messageFactory;
        private readonly ILogger<RedisQueue<TData>> _logger;

        public RedisQueue(IRedisCacheStore redisCache, IQueueMessageFactory messageFactory, ILogger<RedisQueue<TData>> logger)
        {
            _redisCache = redisCache;
            _messageFactory = messageFactory;
            _logger = logger;
        }

        public async Task<string> EnqueueAsync(TData data, string messageId = null)
        {
            try
            {
                var message = _messageFactory.Create(data, messageId);
                var key = QueueHelpers.GetRedisKey<TData>();

                var count = await _redisCache.ListRightPushAsync(key, message).ConfigureAwait(false);
                _logger.LogDebug($"Current number of elements in the {key} queue: {count}");

                return message.Id;
            }
            catch (Exception e)
            {
                throw HandleException(nameof(EnqueueAsync), "Error while adding message in the queue.", e);
            }
        }

        public async Task<IQueueMessage<TData>> DequeueAsync()
        {
            try
            {
                return await _redisCache.ListLeftPopAsync<QueueMessage<TData>>(QueueHelpers.GetRedisKey<TData>())
                           .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw HandleException(nameof(DequeueAsync), "Error while fetching message from the queue.", e);
            }
        }

        public async Task<IEnumerable<IQueueMessage<TData>>> GetManyAsync(long start = 0, long stop = -1)
        {
            try
            {
                return await _redisCache.GetRangeAsync<QueueMessage<TData>>(QueueHelpers.GetRedisKey<TData>(), start, stop)
                           .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw HandleException(nameof(GetManyAsync), "Error while fetching messages from the queue.", e);
            }
        }

        public async Task<long> GetTotalItemCountAsync()
        {
            try
            {
                return await _redisCache.ListLengthAsync(QueueHelpers.GetRedisKey<TData>())
                           .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw HandleException(nameof(GetManyAsync), "Error while fetching queue length.", e);
            }
        }

        private MessageQueueException HandleException(string methodName, string message, Exception e)
        {
            _logger.LogError(message, e);

            return new MessageQueueException(message, e);
        }
    }
}
