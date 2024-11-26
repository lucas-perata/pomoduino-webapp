using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimerApp.Entity;
using TimerApp.Services;

namespace TimerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntryController : ControllerBase
    {
        private readonly TimeEntryService _timeEntryService;

        public TimeEntryController(TimeEntryService timeEntryService)
        {
            _timeEntryService = timeEntryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TimeEntry>>> GetAllTimeEntries()
        {
            var timeEntries = await _timeEntryService.GetAllTimeEntries();

            if(timeEntries is not null)
                return Ok(timeEntries);

            return NotFound();
        }

        [HttpGet("timeEntries")]
        public async Task<ActionResult<List<TimeEntry>>> GetDailyTimeEntries()
        {
            var timeEntries = await _timeEntryService.GetDailyTimeEntries();

            if (timeEntries is not null)
                return Ok(timeEntries);

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeEntry(TimeEntry timeEntry)
        {
            if (timeEntry is not null)
            {
                await _timeEntryService.AddTimeEntry(timeEntry);
                return Ok(timeEntry);
            }

            return BadRequest("Error al crear timeEntry");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTimeEntry(int id, TimeEntry timeEntry)
        {
            var updatedTimeEntry = await _timeEntryService.EditTimeEntry(id, timeEntry);
            if (updatedTimeEntry != null)
            {
                return Ok(updatedTimeEntry);
            }

            return NotFound(); 
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTimeEntry(int id)
        {
            var timeEntry =  await _timeEntryService.GetTimeEntry(id);

            if (timeEntry is null)
                return BadRequest();

            await _timeEntryService.DeleteTimeEntry(timeEntry.Id);

            return NoContent();
        }
    }
}
