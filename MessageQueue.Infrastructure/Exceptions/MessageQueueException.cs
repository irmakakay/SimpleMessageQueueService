namespace MessageQueue.Infrastructure.Exceptions
{
    using System;

    public class MessageQueueException : Exception
    {
        public MessageQueueException()
        {
        }

        public MessageQueueException(string message) : base(message)
        {
        }

        public MessageQueueException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
