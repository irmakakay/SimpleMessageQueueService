namespace MessageQueue.Service.Model
{
    using System;
    using MessageQueue.Common;

    public class ServiceVersionResponse : IProcessedItemInBatch
    {
        public Guid ServiceId { get; set; }

        public string Text { get; set; }
        
        public string ItemDescription { get; set; }
    }
}
