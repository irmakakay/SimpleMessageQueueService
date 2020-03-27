namespace MessageQueue.Infrastructure.Extensions
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MessageQueue.Common;
    using MessageQueue.Common.Extensions;

    public static class QueueExtensions
    {
        private const int Size = 10000;

        public static async Task<IQueueMessage<T>[]> DequeueManyAsync<T>(this IQueue<T> queue, int limit = Size)
            where T : IMessageData
        {
            var bag = new ConcurrentBag<Task<IQueueMessage<T>>>();

            Enumerable.Range(0, limit).ForEach(_ => bag.Add(queue.DequeueAsync()));

            return await Task.WhenAll(bag.AsEnumerable()).ConfigureAwait(false);
        }

        public static IEnumerable<Task<IQueueMessage<T>>> DequeueMany<T>(this IQueue<T> queue, int limit = Size)
            where T : IMessageData
        {
            return Enumerable.Range(0, limit).Select(_ => queue.DequeueAsync());
        }
    }
}