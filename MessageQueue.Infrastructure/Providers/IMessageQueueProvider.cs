namespace MessageQueue.Infrastructure.Providers
{
    using MessageQueue.Common;

    public interface IMessageQueueProvider
    {
        /// <summary>
        /// Get the queue instance based on the generic type passed.
        /// </summary>
        /// <typeparam name="TData">The type a specific queue operates on.</typeparam>
        /// <returns>The queue instance for a given type.</returns>
        IQueue<TData> GetQueue<TData>() where TData : IMessageData;
    }
}