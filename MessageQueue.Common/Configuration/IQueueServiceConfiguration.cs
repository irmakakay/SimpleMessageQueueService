namespace MessageQueue.Common.Configuration
{
    public interface IQueueServiceConfiguration
    {
        bool ProducerEnabled { get; set; }

        bool ConsumerEnabled { get; set; }

        int BatchSize { get; set; }

        int ProcessBatchSize { get; set; }

        bool RetryInPushOrPop { get; set; }

        bool RetryInMessageProcess { get; set; }

        bool ProcessInBatches { get; set; }
    }
}