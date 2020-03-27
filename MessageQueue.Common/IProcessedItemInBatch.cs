namespace MessageQueue.Common
{
    public interface IProcessedItemInBatch
    {
        string ItemDescription { get; }
    }
}