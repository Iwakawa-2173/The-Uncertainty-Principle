using TUP.WebApi.Domain.Entities;

namespace TUP.WebApi.Domain.Interfaces
{
    public interface IScoreScaleService
    {
        Task<Guid> InsertAsync(ScoreScale entity);
        Task<ScoreScale> GetByIdAsync(Guid id);
        Task<IEnumerable<ScoreScale>> GetAllAsync();
        Task UpdateAsync(Guid id, ScoreScale entity);
        Task DeleteAsync(Guid id);
    }
}