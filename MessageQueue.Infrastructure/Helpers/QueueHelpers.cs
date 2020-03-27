namespace MessageQueue.Infrastructure.Helpers
{
    using MessageQueue.Common;

    public static class QueueHelpers
    {
        private const string KeyPrefix = "redisqueue";

        public static string GetRedisKey<TData>() where TData : IMessageData => $"{KeyPrefix}::{typeof(TData).Name}";
    }
}