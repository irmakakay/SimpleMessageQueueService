namespace MessageQueue.Service.Exceptions
{
    using System;

    public class MessageFetchAndProcessException : Exception
    {
        public MessageFetchAndProcessException()
        {
        }

        public MessageFetchAndProcessException(string message) : base(message)
        {
        }

        public MessageFetchAndProcessException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
