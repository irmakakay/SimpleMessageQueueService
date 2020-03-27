namespace MessageQueue.Service.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MessageQueue.Common;
    using MessageQueue.Common.Extensions;
    using MessageQueue.Infrastructure;
    using MessageQueue.Infrastructure.Exceptions;
    using MessageQueue.Infrastructure.Providers;
    using MessageQueue.Logging;
    using MessageQueue.Service.Configuration;
    using MessageQueue.Service.Exceptions;
    using MessageQueue.Service.Helpers;
    using MessageQueue.Service.Validators;
    using Polly;
    using Polly.Retry;

    public abstract class BaseQueueService<TData, TResult> : IQueueService<TData, TResult>
        where TData : IMessageData
        where TResult : IProcessedItemInBatch
    {
        private const int MaxSize = 10000;

        private readonly IConfigurationService _configurationService;
        private readonly Lazy<IQueue<TData>> _lazyQueue;
        private readonly IQueueMessageValidator<TData> _requestValidator;
        private readonly ILogger _logger;

        protected BaseQueueService(
            IMessageQueueProvider queueProvider,
            IConfigurationService configurationService,
            IQueueMessageValidator<TData> requestValidator,
            ILogger logger)
        {
            _configurationService = configurationService;
            _lazyQueue = new Lazy<IQueue<TData>>(queueProvider.GetQueue<TData>);
            _requestValidator = requestValidator;
            _logger = logger;
        }

        public virtual async Task<string> AddDataAsync(TData data, string messageId = null)
        {
            try
            {
                if (!Validate(data)) throw new QueueMessageValidationException(data.ToString());

                _logger.Debug($"Adding [{data}] to the queue.");

                return await GetQueueExceptionPolicy()
                    .ExecuteAsync(async () => await MessageQueue.EnqueueAsync(data, messageId).ConfigureAwait(false))
                    .ConfigureAwait(false);
            }
            catch (MessageQueueException e)
            {
                _logger.Error("Error while adding message to the queue.", e);

                throw;
            }
        }

        public virtual async Task<QueueServiceBatchResult> FetchAndProcessDataAsync(
            Func<TData, Task<TResult>> processFunc,
            long totalCount)
        {
            var batchResult = new QueueServiceBatchResult();

            foreach (var fetchTask in DequeueMany(totalCount, Configuration.BatchSize))
            {
                IQueueMessage<TData> message = null;
                try
                {
                    message = await GetQueueExceptionPolicy()
                        .ExecuteAsync(async () => await fetchTask.ConfigureAwait(false))
                        .ConfigureAwait(false);

                    if (!Validate(message))
                    {
                        _requestValidator.HandleInvalidMessage(message, batchResult, nameof(FetchAndProcessDataAsync));
                        continue;
                    }

                    var result = await Policy.Handle<MessageFetchAndProcessException>()
                        .WaitAndRetryAsync(GetRetrySleepDurations(Configuration.RetryInMessageProcess))
                        .ExecuteAsync(async () => await QueueServiceMessageHelpers
                            .ProcessMessageAsync(message, processFunc)
                            .ConfigureAwait(false))
                        .ConfigureAwait(false);

                    _logger.Debug(result.ItemDescription);

                    batchResult.SuccessCount++;
                }
                catch (MessageQueueException e)
                {
                    _logger.Error("Error while fetching queue item. Item will remain in the queue.", e);

                    batchResult.FailureCount++;
                }
                catch (MessageFetchAndProcessException e)
                {
                    _logger.Error(
                        "Error while processing message data. Adding the data back to the message queue for the next run.", e);

                    batchResult.FailureCount++;

                    if (message == null)
                    {
                        _logger.Warn("Queue message is null, skipping.");
                        continue;
                    }

                    await MessageQueue.EnqueueAsync(message.Data, message.Id).ConfigureAwait(false);
                }
            }

            return batchResult;
        }

        public virtual async Task<QueueServiceBatchResult> FetchAndProcessDataManyAsync(
            Func<IEnumerable<TData>, Task<int>> processFunc,
            long totalCount)
        {
            var batchResult = new QueueServiceBatchResult();

            var bag = new ConcurrentBag<IQueueMessage<TData>>();
            
            foreach (var fetchTask in DequeueMany(totalCount, Configuration.BatchSize))            
            {
                try
                {
                    var message = await GetQueueExceptionPolicy()
                        .ExecuteAsync(async () => await fetchTask.ConfigureAwait(false));

                    if (!Validate(message))
                    {
                        _requestValidator.HandleInvalidMessage(message, batchResult, nameof(FetchAndProcessDataManyAsync));                        
                        continue;
                    }

                    bag.Add(message);
                }
                catch (MessageQueueException e)
                {
                    _logger.Error("Error while fetching queue item. Item will remain in the queue.", e);

                    batchResult.FailureCount++;
                }
            }

            foreach (var processBatch in bag.Partition(Configuration.ProcessBatchSize))
            {
                try
                {
                    var count = await QueueServiceMessageHelpers
                        .ProcessMessagesAsync(processBatch, processFunc).ConfigureAwait(false);

                    batchResult.SuccessCount += Math.Max(0, count);
                }
                catch (MessageFetchAndProcessException e)
                {
                    _logger.Error(
                        "Error while processing messages. Adding the data back to the message queue for the next run.", e);

                    batchResult.FailureCount += processBatch.Count;

                    processBatch.ForEach(async message =>
                        await AddDataAsync(message.Data, message.Id).ConfigureAwait(false));
                }
            }

            return batchResult;
        }

        public async Task<IEnumerable<TData>> GetManyAsync(long startIndex = 0)
        {
            return await GetManyAsync(m => m.Select(_ => _.Data), startIndex)
                       .ConfigureAwait(false);
        }

        public async Task<IEnumerable<string>> GetManyAsStringAsync(long startIndex = 0)
        {
            return await GetManyAsync(m => m.Select(_ => _.Data.MessageDescriptor), startIndex)
                       .ConfigureAwait(false);
        }

        public async Task<long> GetTotalItemCountAsync()
        {
            try
            {
                return await MessageQueue.GetTotalItemCountAsync().ConfigureAwait(false);
            }
            catch (MessageQueueException e)
            {
                _logger.Error("Error while fetching queue length.", e);

                throw;
            }
        }

        protected IQueueMessageValidator<TData> RequestValidator => _requestValidator;

        protected abstract IEnumerable<TimeSpan> GetRetrySleepDurations(bool configValue);

        protected abstract bool Validate(IQueueMessage<TData> message);

        protected abstract bool Validate(TData messageData);

        private IEnumerable<Task<IQueueMessage<TData>>> DequeueMany(long totalCount, long limit = MaxSize)
        {            
            return Enumerable
                .Range(0, (int) Math.Min(limit, totalCount))
                .Select(_ => MessageQueue.DequeueAsync());
        }

        private IQueue<TData> MessageQueue => _lazyQueue.Value;

        private IQueueServiceConfiguration Configuration =>
            _configurationService.GetServiceConfiguration();

        private AsyncRetryPolicy GetQueueExceptionPolicy() =>
            Policy.Handle<MessageQueueException>()
                .WaitAndRetryAsync(GetRetrySleepDurations(Configuration.RetryInPushOrPop));

        private async Task<IEnumerable<T>> GetManyAsync<T>(
            Func<IEnumerable<IQueueMessage<TData>>, IEnumerable<T>> func,
            long startIndex = 0)
        {
            try
            {
                var messages = await MessageQueue.GetManyAsync(startIndex, startIndex + Configuration.BatchSize)
                    .ConfigureAwait(false);

                return func(messages);
            }
            catch (MessageQueueException e)
            {
                _logger.Error("Error while reading queue items.", e);

                throw;
            }
        }
    }
}
