namespace MessageQueue.Service.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MessageQueue.Common;

    public interface IQueueService<TData, TResult>
        where TData : IMessageData
        where TResult : IProcessedItemInBatch
    {
        Task<string> AddDataAsync(TData data, string messageId = null);

        Task<QueueServiceBatchResult> FetchAndProcessDataAsync(Func<TData, Task<TResult>> processFunc,
            long totalCount);

        Task<QueueServiceBatchResult> FetchAndProcessDataManyAsync(Func<IEnumerable<TData>, Task<int>> processFunc,
            long totalCount);

        Task<IEnumerable<TData>> GetManyAsync(long startIndex = 0);

        Task<IEnumerable<string>> GetManyAsStringAsync(long startIndex = 0);

        Task<long> GetTotalItemCountAsync();
    }
}