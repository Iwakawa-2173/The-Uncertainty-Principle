using TUP.WebApi.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace TUP.WebApi.Infrastructure.Repositories
{
    public class ChatMessageRepository : RepositoryBase<ChatMessage>
    {
       public ChatMessageRepository(IConfiguration configuration) : base(configuration) {}

       public override async Task DeleteAsync(long id)
       {
           var sql =$"DELETE FROM chat_messages WHERE message_id={id}";
           await ExecuteSqlAsync(sql);
       }

       public override async Task<IEnumerable<ChatMessage>> GetAllAsync()
       {
           var sql ="SELECT * FROM chat_messages";
           return await ExecuteSqlReaderAsync(sql);
       }

       public override async Task<ChatMessage> GetByIdAsync(long id)
       {
           var sql =$"SELECT * FROM chat_messages WHERE message_id={id}";
           var messages=await ExecuteSqlReaderAsync(sql);
           return messages.FirstOrDefault();
       }

       public override async Task<long> InsertAsync(ChatMessage entity)
       {
           var sql =$"INSERT INTO chat_messages (player_id, message) OUTPUT INSERTED.message_id VALUES ({entity.PlayerId}, '{entity.Message}')";
           return await ExecuteSqlAsync(sql);
       }

       public override async Task UpdateAsync(long id, ChatMessage entity)
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