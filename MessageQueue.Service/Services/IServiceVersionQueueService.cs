namespace MessageQueue.Service.Services
{
    using MessageQueue.Service.Model;

    public interface IServiceVersionQueueService
        : IQueueService<ServiceVersionRequest, ServiceVersionResponse>
    {
    }
}