namespace MessageQueue.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Authentication;
    using System.Threading.Tasks;
    using MessageQueue.Common.Configuration;
    using MessageQueue.Common.Mapping;
    using MessageQueue.Common.Model;
    using MessageQueue.Service.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class QueueProducerController : ControllerBase
    {
        private readonly Lazy<IQueueServiceConfiguration> _lazyServiceConfiguration;
        private readonly IServiceVersionQueueService _queueService;
        private readonly IMappingContext _mappingContext;
        private readonly ILogger<QueueProducerController> _logger;

        public QueueProducerController(
            IConfigurationService configurationService, 
            IServiceVersionQueueService queueService,
            IMappingContext mappingContext,
            ILogger<QueueProducerController> logger)
        {
            _lazyServiceConfiguration = new Lazy<IQueueServiceConfiguration>(configurationService.GetServiceConfiguration);
            _queueService = queueService;
            _mappingContext = mappingContext;
            _logger = logger;
        }

        [HttpPost]
        //[ResponseType(typeof(ServiceVersionResponse))]
        //[ValidationModelStateFilter]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> AddAsync(ServiceVersionRequest incoming)
        {
            if (incoming == null) return BadRequest();

            ServiceVersionResponse response;
            if (Configuration.ProducerEnabled)
            {
                var messageId = await _queueService.AddDataAsync(incoming).ConfigureAwait(false);
                
                _logger.LogDebug($"messageId: {messageId}");

                return CreatedAtAction(nameof(AddAsync), 
                    _mappingContext.MapFromMessageData<ServiceVersionRequest, ServiceVersionResponse>(incoming));
            }

            return NotFound(incoming);
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogDebug("Testing my shit!!!");

            throw new AuthenticationException("Some interesting error!");
        }

        private IQueueServiceConfiguration Configuration => _lazyServiceConfiguration.Value;
    }
}
