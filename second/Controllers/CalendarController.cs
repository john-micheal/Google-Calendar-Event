using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Google_Calendar_events_app.DTO;
using Google_Calendar_events_app.Helper;
using Google_Calendar_events_app.Validation;

namespace Google_Calendar_events_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        [HttpPost("CreateGoogleCalendar")]
        public async Task<IActionResult> CreateGoogleCalendar([FromBody] CreateGoogleEventDto request)
        {
            var requestValidate = request.GoogleCalenderValidate();
            if (!string.IsNullOrEmpty(requestValidate))
            {
                return BadRequest(requestValidate);
            }
            var calenderEvent = await GoogleCalenderHelper.CreateGoogleCalendar(request);
            return Created("", calenderEvent);
        }


        [HttpGet("GetAllCalendare")]
        public async Task<IActionResult> GetAllCalendare()
        {
            return Ok(await GoogleCalenderHelper.GetAllGoogleCalendarEvent());
        }


        [HttpGet("GetCalendareEvent{searchKeyword}")]
        public async Task<IActionResult> GetCalendareEvent(string searchKeyword)
        {
            return Ok(await GoogleCalenderHelper.GetCalendareEvent(searchKeyword));
        }

        [HttpGet("RemoveCalendareEvent{eventId}")]
        public async Task<IActionResult> RemoveCalendareEvent(string eventId)
        {
            var result = await GoogleCalenderHelper.RemoveGoogleCalendarEvent(eventId);
            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);
            return Ok();
        }

     

    }
}