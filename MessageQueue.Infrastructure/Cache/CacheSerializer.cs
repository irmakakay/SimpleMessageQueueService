namespace MessageQueue.Infrastructure.Cache
{
    using System;
    using System.Net.Http.Headers;
    using Newtonsoft.Json;

    public class CacheSerializer : ICacheSerializer
    {
        public string Serialize<T>(T value)
        {
            if (value is MediaTypeHeaderValue)
            {
                return value.ToString();
            }

            return JsonConvert.SerializeObject(value);
        }

        public T Deserialize<T>(string serializedValue) => (T)Deserialize(serializedValue, typeof(T));

        public object Deserialize(string serializedValue, Type type)
        {
            if (string.IsNullOrWhiteSpace(serializedValue))
            {
                return null;
            }

            if (type == typeof(MediaTypeHeaderValue))
            {
                return MediaTypeHeaderValue.Parse(serializedValue);
            }

            return JsonConvert.DeserializeObject(serializedValue, type);
        }
    }
}
