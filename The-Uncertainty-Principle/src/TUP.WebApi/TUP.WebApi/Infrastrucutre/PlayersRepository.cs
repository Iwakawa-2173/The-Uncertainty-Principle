using TUP.WebApi.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace TUP.WebApi.Infrastructure.Repositories
{
    public class PlayerRepository : RepositoryBase<Player>
    {
        public PlayerRepository(IConfiguration configuration) : base(configuration) { }

        public override async Task DeleteAsync(long id)
        {
            var sql = $"DELETE FROM players WHERE player_id = {id}";
            await ExecuteSqlAsync(sql);
        }

        public override async Task<IEnumerable<Player>> GetAllAsync()
        {
            var sql = "SELECT * FROM players";
            return await ExecuteSqlReaderAsync(sql);
        }

        public override async Task<Player> GetByIdAsync(long id)
        {
            var sql = $"SELECT * FROM players WHERE player_id = {id}";
            var players = await ExecuteSqlReaderAsync(sql);
            return players.FirstOrDefault();
        }

        public override async Task<long> InsertAsync(Player entity)
        {
            var sql = $"INSERT INTO players (unique_player_name) OUTPUT INSERTED.player_id VALUES ('{entity.UniquePlayerName}')";
            return await ExecuteSqlAsync(sql);
        }

        public override async Task UpdateAsync(long id, Player entity)
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