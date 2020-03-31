namespace MessageQueue.Common.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using MessageQueue.Common;

    public class ServiceVersionRequest : IMessageData
    {
        [Required]
        public Guid ServiceId { get; set; }

        [Required]
        public string MessageDescriptor { get; set; }

        public string Text { get; set; }

        public string ItemDescription { get; set; }
    }
}
