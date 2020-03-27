namespace MessageQueue.Infrastructure.Factories
{
    using System;
    using MessageQueue.Common;
    using MessageQueue.Infrastructure.Providers;

    public class QueueMessageFactory : IQueueMessageFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public QueueMessageFactory(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public QueueMessage<TData> Create<TData>(TData data, string messageId = null) where TData : IMessageData
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return new QueueMessage<TData>(messageId ?? Guid.NewGuid().ToString("N"), data, _dateTimeProvider.UtcNow);
        }
    }
}
