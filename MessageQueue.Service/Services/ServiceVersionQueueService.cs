namespace MessageQueue.Service.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MessageQueue.Common.Model;
    using MessageQueue.Infrastructure;
    using MessageQueue.Infrastructure.Providers;
    using MessageQueue.Service.Validators;
    using Microsoft.Extensions.Logging;

    public class ServiceVersionQueueService 
        : BaseQueueService<ServiceVersionRequest, ServiceVersionResponse>, IServiceVersionQueueService
    {
        public ServiceVersionQueueService(
            IMessageQueueProvider queueProvider,
            IConfigurationService configurationService,
            IQueueMessageValidator<ServiceVersionRequest> requestValidator,
            ILogger logger)
            : base(queueProvider, configurationService, requestValidator, logger)
        {
        }

        public override Task<string> AddDataAsync(ServiceVersionRequest data, string messageId = null)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return base.AddDataAsync(data, messageId);
        }

        protected override IEnumerable<TimeSpan> GetRetrySleepDurations(bool configValue)
        {
            if (!configValue) return Enumerable.Empty<TimeSpan>();

            return new[]
            {
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(5)
            };
        }

        protected override bool Validate(IQueueMessage<ServiceVersionRequest> message) => RequestValidator.Validate(message?.Data);

        protected override bool Validate(ServiceVersionRequest messageData) => RequestValidator.Validate(messageData);
    }
}