namespace MessageQueue.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Security.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class QueueConsumerController : ControllerBase
    {
        private readonly ILogger<QueueConsumerController> _logger;

        public QueueConsumerController(ILogger<QueueConsumerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogDebug("Testing my shit!!!");

            throw new AuthenticationException();
        }
    }
}
