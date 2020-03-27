namespace MessageQueue.Common
{
    using System;

    public interface IMessageSerializer
    {
        string Serialize<T>(T value);

        T Deserialize<T>(string serializedValue);

        object Deserialize(string serializedValue, Type type);
    }
}
