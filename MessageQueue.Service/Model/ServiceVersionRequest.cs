namespace MessageQueue.Service.Model
{
    using System;
    using MessageQueue.Common;

    public class ServiceVersionRequest : IMessageData
    {
        public Guid Id { get; set; }

        public string MessageDescriptor { get; set; }
    }
}
