using TUP.WebApi.Domain.Entities;

namespace TUP.WebApi.Domain.Interfaces
{
    public interface IChatMessageService
    {
        Task<long> InsertAsync(ChatMessage entity);
        Task<ChatMessage> GetByIdAsync(long id);
        Task<IEnumerable<ChatMessage>> GetAllAsync();
        Task UpdateAsync(long id, ChatMessage entity);
        Task DeleteAsync(long id);
    }
}