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
        private readonly IRepository<Event> eventsRepository;

        public EventsController(ILogger<EventsController> logger, IRepository<Event> eventsRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.eventsRepository = eventsRepository ?? throw new ArgumentNullException(nameof(eventsRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            this.logger.LogInformation("get all events");
            return Ok(await eventsRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var eventItem = await eventsRepository.GetByIdAsync(id);
            if (eventItem == null) return NotFound();
            return Ok(eventItem);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] Event eventItem)
        {
            if (string.IsNullOrWhiteSpace(eventItem.Description))
                return BadRequest("Укажите описание события!");

            return Ok(await eventsRepository.InsertAsync(eventItem));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] Event eventItem)
        {
            var existingEvent = await eventsRepository.GetByIdAsync(id);
            if (existingEvent == null) return NotFound();

            existingEvent.Description = eventItem.Description;
            existingEvent.IsSignificant = eventItem.IsSignificant;
            
            await eventsRepository.UpdateAsync(id, existingEvent);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var existingEvent = await eventsRepository.GetByIdAsync(id);
            if (existingEvent == null) return NotFound();

            await eventsRepository.DeleteAsync(id);
            return Ok();
        }
    }
}