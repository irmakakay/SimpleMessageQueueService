namespace MessageQueue.Service
{
    public class QueueServiceBatchResult
    {
        public long TotalCount => SuccessCount + FailureCount + NullMessageCount;

        public long NullMessageCount { get; set; }

        public long SuccessCount { get; set; }

        public long FailureCount { get; set; }

        public override string ToString() =>
            $"Total Count: {TotalCount}, Success Count: {SuccessCount}, Failure Count: {FailureCount}, Null Message Count: {NullMessageCount}";
    }
}
