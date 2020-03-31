namespace MessageQueue.Common.Model
{
    using System;
    using MessageQueue.Common;
    using MessageQueue.Common.Mapping;

    public class ServiceVersionResponse : IProcessedItemInBatch, IMapperTarget
    {
        public Guid ServiceId { get; set; }

        public string Text { get; set; }
        
        public string ItemDescription { get; set; }
    }
}
