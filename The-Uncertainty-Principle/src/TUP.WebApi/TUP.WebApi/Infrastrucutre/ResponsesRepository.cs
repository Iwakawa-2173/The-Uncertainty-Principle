using TUP.WebApi.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace TUP.WebApi.Infrastructure.Repositories
{
    public class ResponseRepository : RepositoryBase<Response>
    {
        public ResponseRepository(IConfiguration configuration) : base(configuration) { }

        public override async Task DeleteAsync(long id)
        {
            var sql = $"DELETE FROM responses WHERE response_id = {id}";
            await ExecuteSqlAsync(sql);
        }

        public override async Task<IEnumerable<Response>> GetAllAsync()
        {
            var sql = "SELECT * FROM responses";
            return await ExecuteSqlReaderAsync(sql);
        }

        public override async Task<Response> GetByIdAsync(long id)
        {
            var sql = $"SELECT * FROM responses WHERE response_id = {id}";
            var responses = await ExecuteSqlReaderAsync(sql);
            return responses.FirstOrDefault();
        }

        public override async Task<long> InsertAsync(Response entity)
        {
            var sql = $"INSERT INTO responses (player_id, event_id, response_option) OUTPUT INSERTED.response_id VALUES ({entity.PlayerId}, {entity.EventId}, {entity.ResponseOption})";
            return await ExecuteSqlAsync(sql);
        }

        public override async Task UpdateAsync(long id, Response entity)
        {
            var sql = $"UPDATE responses SET player_id = {entity.PlayerId}, event_id = {entity.EventId}, response_option = {entity.ResponseOption} WHERE response_id = {id}";
            await ExecuteSqlAsync(sql);
        }

        protected override Response GetEntityFromReader(SqlDataReader reader)
        {
            return new Response
            {
                ResponseId = reader.GetInt32(0),
                PlayerId = reader.GetInt32(1),
                EventId = reader.GetInt32(2),
                ResponseOption = reader.GetInt32(3),
                CreatedAt = reader.GetDateTime(4)
            };
        }
    }
}