using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace TUP.WebApi.Infrastructure
{
    public class ScoreScalesRepository : RepositoryBase<ScoreScale>, IRepository<ScoreScale>
    {
		public ScoreScalesRepository(IConfiguration configuration) : base(configuration) { }

        public override async Task DeleteAsync(Guid id)
        {
            var sql = $"DELETE FROM score_scales WHERE scale_id = {id}";
            await ExecuteSqlAsync(sql);
        }

        public override async Task<IEnumerable<ScoreScale>> GetAllAsync()
        {
            var sql = "SELECT * FROM score_scales";
            return await ExecuteSqlReaderAsync(sql);
        }

        public override async Task<ScoreScale> GetByIdAsync(Guid id)
        {
            var sql = $"SELECT * FROM score_scales WHERE scale_id = {id}";
            var scales = await ExecuteSqlReaderAsync(sql);
            return scales.FirstOrDefault();
        }

        public override async Task<Guid> InsertAsync(ScoreScale entity)
        {
			var newId = Guid.NewGuid();
            var sql = $"INSERT INTO score_scales (player_id, scale_1, scale_2, scale_3) OUTPUT INSERTED.scale_id VALUES ({entity.PlayerId}, {entity.Scale1}, {entity.Scale2}, {entity.Scale3})";
            await this.ExecuteSqlAsync(sql);
			return newId;
        }

        public override async Task UpdateAsync(Guid id, ScoreScale entity)
        {
            var sql = $"UPDATE score_scales SET player_id = {entity.PlayerId}, scale_1 = {entity.Scale1}, scale_2 = {entity.Scale2}, scale_3 = {entity.Scale3}, last_event_id={entity.LastEventId} WHERE scale_id={id}";
            await ExecuteSqlAsync(sql);
       }

       protected override ScoreScale GetEntityFromReader(SqlDataReader reader)
       {
           return new ScoreScale
           {
               ScaleId=reader.GetInt32(0),
               PlayerId=reader.GetInt32(1),
               Scale1=reader.GetInt32(2),
               Scale2=reader.GetInt32(3),
               Scale3=reader.GetInt32(4),
               LastEventId=reader.IsDBNull(5)?null:(int?)reader.GetInt32(5), // Nullable int
               UpdatedAt=reader.GetDateTime(6)
           };
       }
   }
}