namespace MessageQueue.Infrastructure.Cache
{
    using System;

    public interface ICacheSerializer
    {
        string Serialize<T>(T value);

        T Deserialize<T>(string serializedValue);

        object Deserialize(string serializedValue, Type type);
    }
}
