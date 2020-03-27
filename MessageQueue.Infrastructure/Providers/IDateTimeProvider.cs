namespace MessageQueue.Infrastructure.Providers
{
    using System;

    /// <summary>
    /// The interface to avoid tight coupling with the static DateTime calls.
    /// </summary>
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}