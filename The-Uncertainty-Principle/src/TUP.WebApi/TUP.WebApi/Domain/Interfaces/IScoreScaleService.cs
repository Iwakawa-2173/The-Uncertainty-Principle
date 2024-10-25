using TUP.WebApi.Domain.Entities;

namespace TUP.WebApi.Domain.Interfaces
{
    public interface IScoreScaleService
    {
        Task<long> InsertAsync(ScoreScale entity);
        Task<ScoreScale> GetByIdAsync(long id);
        Task<IEnumerable<ScoreScale>> GetAllAsync();
        Task UpdateAsync(long id, ScoreScale entity);
        Task DeleteAsync(long id);
    }
}