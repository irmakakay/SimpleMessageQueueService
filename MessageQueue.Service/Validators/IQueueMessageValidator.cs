namespace MessageQueue.Service.Validators
{
    using MessageQueue.Common;
    using MessageQueue.Infrastructure;

    public interface IQueueMessageValidator<in TData> where TData : IMessageData
    {
        bool Validate(IQueueMessage<TData> message);

        bool Validate(TData messageData);

        void HandleInvalidMessage(
            IQueueMessage<TData> message,
            QueueServiceBatchResult result,
            string methodName);
    }
}