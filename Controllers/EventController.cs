using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        //[HttpGet("get-test")]
        //public IActionResult Test()
        //{
        //    var response = _eventService.Test();

        //    return Ok(response);
        //}
    }
}
