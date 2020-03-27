namespace MessageQueue.Infrastructure.Cache
{
    public interface IRedisCacheConfiguration
    {
        string ConnectionString { get; set; }
    }
}