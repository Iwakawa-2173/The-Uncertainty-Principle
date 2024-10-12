using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace The_Uncertainty_Principle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly GameDbContext _context;

        public EventsController(GameDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetEvents()
        {
            return _context.Events.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetEvent(int id)
        {
            var eventItem = _context.Events.Find(id);
            if (eventItem == null) return BadRequest();
            return eventItem;
        }

        [HttpPost]
        public ActionResult<Event> CreateEvent(Event eventItem)
        {
            _context.Events.Add(eventItem);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetEvent), new { id = eventItem.EventId }, eventItem);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, Event eventItem)
        {
            if (id != eventItem.EventId) return BadRequest();
            _context.Entry(eventItem).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var eventItem = _context.Events.Find(id);
            if (eventItem == null) return BadRequest();
            _context.Events.Remove(eventItem);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
