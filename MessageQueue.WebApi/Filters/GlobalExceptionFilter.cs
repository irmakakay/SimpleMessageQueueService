namespace MessageQueue.WebApi.Filters
{
    using System.Net;
    using MessageQueue.Infrastructure.Exceptions;
    using MessageQueue.Service.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class GlobalExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is QueueMessageValidationException validationException)
            {
                context.Result = new ObjectResult(validationException.Message)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
                context.ExceptionHandled = true;
            }

            else if (context.Exception is MessageQueueException queueException)
            {
                context.Result = new ObjectResult(queueException.Message)
                {
                    StatusCode = (int)HttpStatusCode.BadGateway
                };
                context.ExceptionHandled = true;
            }

            // TODO - Add more here.
        }
    }
}
