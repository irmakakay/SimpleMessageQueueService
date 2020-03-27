namespace MessageQueue.Infrastructure.Providers
{
    using System;

    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}