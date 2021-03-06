﻿namespace MessageQueue.Common.Model
{
    using System.ComponentModel.DataAnnotations;
    using MessageQueue.Common;
    using MessageQueue.Common.Mapping;

    public class ServiceVersionRequest : IMessageData, IMapperSource
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public string MessageDescriptor { get; set; }

        public string Text { get; set; }

        public string ItemDescription { get; set; }

        public override string ToString() =>
            $"ServiceId: {ServiceId}, MessageDescriptor: {MessageDescriptor}";
    }
}
