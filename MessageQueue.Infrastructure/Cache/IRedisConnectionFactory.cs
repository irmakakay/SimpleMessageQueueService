namespace MessageQueue.Infrastructure.Cache
{
    using System.Threading.Tasks;
    using StackExchange.Redis;

    public interface IRedisConnectionFactory
    {
        Task<IDatabase> GetDatabaseAsync();
    }
}
