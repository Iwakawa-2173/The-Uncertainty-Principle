using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace TUP.WebApi.Infrastructure
{
    public class PlayersRepository : RepositoryBase<Player>, IRepository<Player>
    {
		public PlayersRepository(IConfiguration configuration) : base(configuration) { }
		
        public override async Task DeleteAsync(Guid id)
        {
            var sql = $"DELETE FROM players WHERE player_id = {id}";
            await ExecuteSqlAsync(sql);
        }

        public override async Task<IEnumerable<Player>> GetAllAsync()
        {
            var sql = "SELECT * FROM players";
            return await ExecuteSqlReaderAsync(sql);
        }

        public override async Task<Player> GetByIdAsync(Guid id)
        {
            var sql = $"SELECT * FROM players WHERE player_id = {id}";
            var players = await ExecuteSqlReaderAsync(sql);
            return players.FirstOrDefault();
        }

        public override async Task<Guid> InsertAsync(Player entity)
        {
            var newId = Guid.NewGuid();
            var sql = $"INSERT INTO players (unique_player_name) OUTPUT INSERTED.player_id VALUES ('{entity.UniquePlayerName}')";
            await this.ExecuteSqlAsync(sql);
			return newId;
        }

        public override async Task UpdateAsync(Guid id, Player entity)
        {
            var sql = $"UPDATE players SET unique_player_name = '{entity.UniquePlayerName}' WHERE player_id = {id}";
            await ExecuteSqlAsync(sql);
        }

        protected override Player GetEntityFromReader(SqlDataReader reader)
        {
            return new Player
            {
                PlayerId = reader.GetInt32(0),
                UniquePlayerName = reader.GetString(1),
                CreatedAt = reader.GetDateTime(2)
            };
        }
    }
}