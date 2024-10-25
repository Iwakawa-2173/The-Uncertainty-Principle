using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;

namespace TUP.WebApi.Domain.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _repository;

        public EventService(IRepository<Event> eventRepository)
        {
            this._repository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public async Task DeleteAsync(long id)
        {
            await this._repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await this._repository.GetAllAsync();
        }

        public async Task<Event> GetByIdAsync(long id)
        {
            var eventEntity = await this._repository.GetByIdAsync(id);
            if (eventEntity == null) throw new KeyNotFoundException(nameof(eventEntity));
            return eventEntity;
        }

        public async Task<long> InsertAsync(Event entity)
        {
            return await this._repository.InsertAsync(entity);
        }

        public async Task UpdateAsync(long id, Event entity)
        {
            await this._repository.UpdateAsync(id, entity);
        }
    }
}