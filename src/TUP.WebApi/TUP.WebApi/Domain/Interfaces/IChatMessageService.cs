using TUP.WebApi.Domain.Entities;

namespace TUP.WebApi.Domain.Interfaces
{
    public interface IChatMessageService
    {
        Task<Guid> InsertAsync(ChatMessage entity);
        Task<ChatMessage> GetByIdAsync(Guid id);
        Task<IEnumerable<ChatMessage>> GetAllAsync();
        Task UpdateAsync(Guid id, ChatMessage entity);
        Task DeleteAsync(Guid id);
    }
}