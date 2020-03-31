namespace MessageQueue.Service.Services
{
    using MessageQueue.Common.Model;

    public interface IServiceVersionQueueService
        : IQueueService<ServiceVersionRequest, ServiceVersionResponse>
    {
    }
}