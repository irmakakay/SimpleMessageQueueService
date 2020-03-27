namespace MessageQueue.Infrastructure.Extensions
{
    using StackExchange.Redis;

    public static class RedisExtensions
    {
        public static bool IsOpen(this ConnectionMultiplexer multiplexer) => multiplexer != null && multiplexer.IsConnected;
    }
}
