namespace MessageQueue.Configuration.Sections
{
    public interface IRedisCacheConfiguration
    {
        string ConnectionString { get; set; }
    }
}