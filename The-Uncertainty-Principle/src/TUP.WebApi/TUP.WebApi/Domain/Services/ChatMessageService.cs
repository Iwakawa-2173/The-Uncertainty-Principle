using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;

namespace TUP.WebApi.Domain.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IRepository<ChatMessage> _repository;

        public ChatMessageService(IRepository<ChatMessage> chatMessageRepository)
        {
            this._repository = chatMessageRepository ?? throw new ArgumentNullException(nameof(chatMessageRepository));
        }

        public async Task DeleteAsync(long id)
        {
            await this._repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ChatMessage>> GetAllAsync()
        {
            return await this._repository.GetAllAsync();
        }

        public async Task<ChatMessage> GetByIdAsync(long id)
        {
            var message = await this._repository.GetByIdAsync(id);
            if (message == null) throw new KeyNotFoundException(nameof(message));
            return message;
        }

        public async Task<long> InsertAsync(ChatMessage entity)
        {
            return await this._repository.InsertAsync(entity);
        }

        public async Task UpdateAsync(long id, ChatMessage entity)
        {
            await this._repository.UpdateAsync(id, entity);
        }
    }
}