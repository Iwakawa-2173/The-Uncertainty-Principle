using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;

namespace TUP.WebApi.Domain.Services
{
    public class ResponseService : IResponseService
    {
        private readonly IRepository<Response> _repository;

        public ResponseService(IRepository<Response> responseRepository)
        {
            this._repository = responseRepository ?? throw new ArgumentNullException(nameof(responseRepository));
        }

        public async Task DeleteAsync(Guid id)
        {
            await this._repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Response>> GetAllAsync()
        {
            return await this._repository.GetAllAsync();
        }

        public async Task<Response> GetByIdAsync(Guid id)
        {
            var response = await this._repository.GetByIdAsync(id);
            if (response == null) throw new KeyNotFoundException(nameof(response));
            return response;
        }

        public async Task<Guid> InsertAsync(Response entity)
        {
            return await this._repository.InsertAsync(entity);
        }

        public async Task UpdateAsync(Guid id, Response entity)
       {
           await this._repository.UpdateAsync(id, entity);
       }
   }
}