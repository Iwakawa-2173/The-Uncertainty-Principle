using TUP.WebApi.Domain.Entities;

namespace TUP.WebApi.Domain.Interfaces
{
    public interface IPlayerService
    {
        Task<long> InsertAsync(Player entity);
        Task<Player> GetByIdAsync(long id);
        Task<IEnumerable<Player>> GetAllAsync();
        Task UpdateAsync(long id, Player entity);
        Task DeleteAsync(long id);
    }
}