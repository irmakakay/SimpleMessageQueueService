namespace MessageQueue.Infrastructure.Factories
{
    using MessageQueue.Common;

    public interface IQueueMessageFactory
    {
        QueueMessage<TData> Create<TData>(TData data, string messageId = null) where TData : IMessageData;
    }
}