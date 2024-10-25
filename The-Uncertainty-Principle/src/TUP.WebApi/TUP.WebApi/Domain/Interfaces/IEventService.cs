using TUP.WebApi.Domain.Entities;

namespace TUP.WebApi.Domain.Interfaces
{
    public interface IEventService
    {
        Task<long> InsertAsync(Event entity);
        Task<Event> GetByIdAsync(long id);
        Task<IEnumerable<Event>> GetAllAsync();
        Task UpdateAsync(long id, Event entity);
        Task DeleteAsync(long id);
    }
}