namespace MessageQueue.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class ErrorController : ControllerBase
    {
        [AllowAnonymous]
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
