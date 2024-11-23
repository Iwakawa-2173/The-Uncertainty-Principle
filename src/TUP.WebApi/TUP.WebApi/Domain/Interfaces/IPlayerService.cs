using TUP.WebApi.Domain.Entities;

namespace TUP.WebApi.Domain.Interfaces
{
    public interface IPlayerService
    {
        Task<Guid> InsertAsync(Player entity);
        Task<Player> GetByIdAsync(Guid id);
        Task<IEnumerable<Player>> GetAllAsync();
        Task UpdateAsync(Guid id, Player entity);
        Task DeleteAsync(Guid id);
    }
}