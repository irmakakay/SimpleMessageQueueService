namespace MessageQueue.Configuration.Sections
{
    public class RedisCacheConfiguration : IRedisCacheConfiguration
    {
        public string ConnectionString { get; set; }
    }
}