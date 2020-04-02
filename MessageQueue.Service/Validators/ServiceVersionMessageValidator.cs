namespace MessageQueue.Service.Validators
{
    using MessageQueue.Common.Model;
    using MessageQueue.Infrastructure;
    using MessageQueue.Service.Exceptions;
    using Microsoft.Extensions.Logging;

    public class ServiceVersionMessageValidator : IQueueMessageValidator<ServiceVersionRequest>
    {
        private readonly ILogger<ServiceVersionMessageValidator> _logger;

        public ServiceVersionMessageValidator(ILogger<ServiceVersionMessageValidator> logger)
        {
            _logger = logger;
        }

        public bool Validate(IQueueMessage<ServiceVersionRequest> message) =>
            Validate(message?.Data);

        public bool Validate(ServiceVersionRequest messageData) =>
            messageData.MessageDescriptor != null;

        public void HandleInvalidMessage(
            IQueueMessage<ServiceVersionRequest> message, 
            QueueServiceBatchResult result,
            string methodName)
        {
            if (message == null)
            {
                result.NullMessageCount++;
                _logger.LogWarning("Queue message is null, skipping.");
            }
            else
            {
                var e = new QueueMessageValidationException(
                    $"Error while validating message. Message Id: {message.Id}");
                
                _logger.LogError(e.Message, e);

                result.FailureCount++;
            }
        }
    }
}
