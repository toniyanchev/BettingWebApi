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

        [HttpGet("get-daily-matches")]
        public IActionResult GetDailyMatches()
        {
            var response = _eventService.GetDailyMatches();

            return Ok(response);
        }

        [HttpGet("get-match/{id}")]
        public IActionResult GetMatche(int id)
        {
            var response = _eventService.GetMatchById(id);

            return response == null
                ? NotFound(response)
                : Ok(response);
        }

        [HttpGet("get-match-by-type/{typeId}")]
        public IActionResult GetMatchesByType(int typeId)
        {
            var response = _eventService.GetMatchesByType(typeId);

            return Ok(response);
        }
    }
}
