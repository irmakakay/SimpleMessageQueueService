namespace MessageQueue.Service.Configuration
{
    public class QueueServiceConfiguration : IQueueServiceConfiguration
    {
        public bool ProducerEnabled { get; set; }

        public bool ConsumerEnabled { get; set; }

        public int BatchSize { get; set; }

        public int ProcessBatchSize { get; set; }

        public bool RetryInPushOrPop { get; set; }

        public bool RetryInMessageProcess { get; set; }

        public bool ProcessInBatches { get; set; }

        public override string ToString() =>
            $"ProducerEnabled: {ProducerEnabled}, ConsumerEnabled: {ConsumerEnabled}, BatchSize: {BatchSize}, ProcessBatchSize: {ProcessBatchSize}, RetryInPushOrPop: {RetryInPushOrPop}, RetryInMessageProcess: {RetryInMessageProcess}, ProcessInBatches: {ProcessInBatches}";
    }
}
