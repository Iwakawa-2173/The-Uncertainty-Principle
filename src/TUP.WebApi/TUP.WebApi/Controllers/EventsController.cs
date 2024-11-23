using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TUP.WebApi.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> logger;
        private readonly IEventService eventService;

        public EventsController(ILogger<EventsController> logger, IEventService eventService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            this.logger.LogInformation("Получение всех событий");
            var events = await eventService.GetAllAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(Guid id)
        {
            try
            {
                var eventItem = await eventService.GetByIdAsync(id);
                return Ok(eventItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Insert([FromBody] Event eventItem)
        {
            if (string.IsNullOrWhiteSpace(eventItem.Description))
                return BadRequest();

            var id = await eventService.InsertAsync(eventItem);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] Event eventItem)
        {
            try
            {
                await eventService.UpdateAsync(id, eventItem);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await eventService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
