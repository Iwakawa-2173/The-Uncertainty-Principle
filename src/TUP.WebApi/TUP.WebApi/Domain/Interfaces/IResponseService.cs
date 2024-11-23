using TUP.WebApi.Domain.Entities;

namespace TUP.WebApi.Domain.Interfaces
{
    public interface IResponseService
    {
        Task<Guid> InsertAsync(Response entity);
        Task<Response> GetByIdAsync(Guid id);
        Task<IEnumerable<Response>> GetAllAsync();
        Task UpdateAsync(Guid id, Response entity);
        Task DeleteAsync(Guid id);
    }
}