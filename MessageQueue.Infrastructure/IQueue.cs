namespace MessageQueue.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MessageQueue.Common;

    public interface IQueue<TData> where TData : IMessageData
    {
        /// <summary>
        /// Wraps generic data object into a message and puts it to queue
        /// </summary>
        /// <param name="data">Generic data object</param>
        /// <param name="messageId">Message Id</param>
        /// <returns>The message identifier</returns>
        Task<string> EnqueueAsync(TData data, string messageId = null);

        /// <summary>
        /// Returns and deletes the item from the head of the queue.   
        /// </summary>
        /// <returns>Generic data object in QueueMessage container</returns>
        Task<IQueueMessage<TData>> DequeueAsync();

        /// <summary>
        /// Returns the range of queue items without deleting them.
        /// </summary>
        /// <param name="start">Starting position in the queue.</param>
        /// <param name="stop">End position in the queue.</param>
        /// <returns>A collection of queue messages.</returns>
        Task<IEnumerable<IQueueMessage<TData>>> GetManyAsync(long start = 0, long stop = -1);

        /// <summary>
        /// Returns the total number of items in the queue.
        /// </summary>
        /// <returns>Total number of queue items.</returns>
        Task<long> GetTotalItemCountAsync();
    }
}
