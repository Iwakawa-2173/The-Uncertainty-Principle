using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;

namespace TUP.WebApi.Domain.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> _repository;

        public PlayerService(IRepository<Player> playerRepository)
        {
            this._repository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public async Task DeleteAsync(long id)
        {
            await this._repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await this._repository.GetAllAsync();
        }

        public async Task<Player> GetByIdAsync(long id)
        {
            var player = await this._repository.GetByIdAsync(id);
            if (player == null) throw new KeyNotFoundException(nameof(player));
            return player;
        }

        public async Task<long> InsertAsync(Player entity)
        {
            return await this._repository.InsertAsync(entity);
        }

        public async Task UpdateAsync(long id, Player entity)
        {
            await this._repository.UpdateAsync(id, entity);
        }
    }
}