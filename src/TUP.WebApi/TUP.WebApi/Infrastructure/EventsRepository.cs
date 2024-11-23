using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace TUP.WebApi.Infrastructure
{
    public class EventsRepository : RepositoryBase<Event>, IRepository<Event>
    {
		public EventsRepository(IConfiguration configuration) : base(configuration) { }

        public override async Task DeleteAsync(Guid id)
        {
            var sql = $"DELETE FROM events WHERE event_id = {id}";
            await ExecuteSqlAsync(sql);
        }

        public override async Task<IEnumerable<Event>> GetAllAsync()
        {
            var sql = "SELECT * FROM events";
            return await ExecuteSqlReaderAsync(sql);
        }

        public override async Task<Event> GetByIdAsync(Guid id)
        {
            var sql = $"SELECT * FROM events WHERE event_id = {id}";
            var events = await ExecuteSqlReaderAsync(sql);
            return events.FirstOrDefault();
        }

        public override async Task<Guid> InsertAsync(Event entity)
        {
			var newId = Guid.NewGuid();
            var sql = $"INSERT INTO events (event_number, is_significant, description) OUTPUT INSERTED.event_id VALUES ({entity.EventNumber}, '{entity.IsSignificant}', '{entity.Description}')";
            await this.ExecuteSqlAsync(sql);
			return newId;
        }

        public override async Task UpdateAsync(Guid id, Event entity)
        {
            var sql = $"UPDATE events SET event_number = {entity.EventNumber}, is_significant = '{entity.IsSignificant}', description = '{entity.Description}' WHERE event_id = {id}";
            await ExecuteSqlAsync(sql);
        }

        protected override Event GetEntityFromReader(SqlDataReader reader)
        {
            return new Event
            {
                EventId = reader.GetInt32(0),
                EventNumber = reader.GetInt32(1),
                IsSignificant = reader.GetBoolean(2),
                Description = reader.GetString(3)
            };
        }
    }
}