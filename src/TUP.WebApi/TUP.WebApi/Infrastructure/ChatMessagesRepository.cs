using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace TUP.WebApi.Infrastructure
{
    public class ChatMessagesRepository : RepositoryBase<ChatMessage>, IRepository<ChatMessage>
    {
	   public ChatMessagesRepository(IConfiguration configuration) : base(configuration) { }

       public override async Task DeleteAsync(Guid id)
       {
           var sql =$"DELETE FROM chat_messages WHERE message_id={id}";
           await ExecuteSqlAsync(sql);
       }

       public override async Task<IEnumerable<ChatMessage>> GetAllAsync()
       {
           var sql ="SELECT * FROM chat_messages";
           return await ExecuteSqlReaderAsync(sql);
       }

       public override async Task<ChatMessage> GetByIdAsync(Guid id)
       {
           var sql =$"SELECT * FROM chat_messages WHERE message_id={id}";
           var messages=await ExecuteSqlReaderAsync(sql);
           return messages.FirstOrDefault();
       }

       public override async Task<Guid> InsertAsync(ChatMessage entity)
       {
		   var newId = Guid.NewGuid();
           var sql =$"INSERT INTO chat_messages (player_id, message) OUTPUT INSERTED.message_id VALUES ({entity.PlayerId}, '{entity.Message}')";
           await this.ExecuteSqlAsync(sql);
		   return newId;
       }

       public override async Task UpdateAsync(Guid id, ChatMessage entity)
       {
           var sql =$"UPDATE chat_messages SET player_id={entity.PlayerId}, message='{entity.Message}' WHERE message_id={id}";
           await ExecuteSqlAsync(sql);
       }

       protected override ChatMessage GetEntityFromReader(SqlDataReader reader)
       {
           return new ChatMessage
           {
               MessageId=reader.GetInt32(0),
               PlayerId=reader.GetInt32(1),
               Message=reader.GetString(2),
               CreatedAt=reader.GetDateTime(3)
           };
       }
   }
}