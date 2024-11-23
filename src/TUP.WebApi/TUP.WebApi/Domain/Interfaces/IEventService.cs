using TUP.WebApi.Domain.Entities;

namespace TUP.WebApi.Domain.Interfaces
{
    public interface IEventService
    {
        Task<Guid> InsertAsync(Event entity);
        Task<Event> GetByIdAsync(Guid id);
        Task<IEnumerable<Event>> GetAllAsync();
        Task UpdateAsync(Guid id, Event entity);
        Task DeleteAsync(Guid id);
    }
}