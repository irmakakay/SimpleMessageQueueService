namespace MessageQueue.Infrastructure
{
    using System;

    /// <summary>
    /// The wrapper around the actual queue items.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface IQueueMessage<out TData>
    {
        /// <summary>
        /// Unique message id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Date when the message instance was created.
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// The actual data wrapped by the message.
        /// </summary>
        TData Data { get; }
    }
}