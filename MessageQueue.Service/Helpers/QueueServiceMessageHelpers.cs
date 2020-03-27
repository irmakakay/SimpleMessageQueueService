namespace MessageQueue.Service.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MessageQueue.Common;
    using MessageQueue.Infrastructure;
    using MessageQueue.Service.Exceptions;

    public static class QueueServiceMessageHelpers
    {
        public static async Task<TResult> ProcessMessageAsync<TData, TResult>(
            IQueueMessage<TData> message,
            Func<TData, Task<TResult>> processFunc)
            where TData : IMessageData
        {
            try
            {
                return await processFunc(message.Data).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new MessageFetchAndProcessException("Error while processing message data.", e);
            }
        }

        public static async Task<TResult> ProcessMessagesAsync<TData, TResult>(
            IEnumerable<IQueueMessage<TData>> messages,
            Func<IEnumerable<TData>, Task<TResult>> processFunc)
            where TData : IMessageData
        {
            try
            {
                return await processFunc(messages.Select(_ => _.Data)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new MessageFetchAndProcessException("Error while processing messages.", e);
            }
        }
    }
}