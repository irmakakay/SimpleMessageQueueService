namespace MessageQueue.Infrastructure.Factories
{
    using System;
    using MessageQueue.Common;

    public class QueueMessage<TData> : IQueueMessage<TData>,
                                       IEquatable<QueueMessage<TData>> where TData : IMessageData
    {
        public string Id { get; }

        public DateTime CreatedAt { get; }

        public TData Data { get; }

        public QueueMessage(string id, TData data, DateTime createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
            Data = data;
        }

        public bool Equals(QueueMessage<TData> other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return string.Equals(Id, other.Id) && CreatedAt.Equals(other.CreatedAt);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((QueueMessage<TData>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ CreatedAt.GetHashCode();
                return hashCode;
            }
        }
    }
}