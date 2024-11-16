using TUP.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TUP.WebApi.Infrastructure.Repositories
{
    public class EventRepository : RepositoryBase<Event>
    {
        private readonly TUPContext _context;

        public EventRepository(TUPContext context) : base(context)
        {
            _context = context;
        }

        public override async Task DeleteAsync(long id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete != null)
            {
                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public override async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public override async Task<Event> GetByIdAsync(long id)
        {
            return await _context.Events.FindAsync(id);
        }

        public override async Task<long> InsertAsync(Event entity)
        {
            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.EventId; // Предполагается, что EventId генерируется автоматически
        }

        public override async Task UpdateAsync(long id, Event entity)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent != null)
            {
                existingEvent.EventNumber = entity.EventNumber;
                existingEvent.IsSignificant = entity.IsSignificant;
                existingEvent.Description = entity.Description;

                _context.Events.Update(existingEvent);
                await _context.SaveChangesAsync();
            }
        }
    }
}
