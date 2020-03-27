namespace MessageQueue.Service.Exceptions
{
    using System;

    public class QueueMessageValidationException : Exception
    {
        public QueueMessageValidationException()
        {
        }

        public QueueMessageValidationException(string message) : base(message)
        {
        }

        public QueueMessageValidationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
