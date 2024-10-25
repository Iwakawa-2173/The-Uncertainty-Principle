using TUP.WebApi.Domain.Entities;

namespace TUP.WebApi.Domain.Interfaces
{
    public interface IResponseService
    {
        Task<long> InsertAsync(Response entity);
        Task<Response> GetByIdAsync(long id);
        Task<IEnumerable<Response>> GetAllAsync();
        Task UpdateAsync(long id, Response entity);
        Task DeleteAsync(long id);
    }
}