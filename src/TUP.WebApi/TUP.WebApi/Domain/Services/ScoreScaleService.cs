using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;

namespace TUP.WebApi.Domain.Services
{
    public class ScoreScaleService : IScoreScaleService
    {
       private readonly IRepository<ScoreScale> _repository;

       public ScoreScaleService(IRepository<ScoreScale> scoreScaleRepository)
       {
           this._repository = scoreScaleRepository ?? throw new ArgumentNullException(nameof(scoreScaleRepository));
       }

       public async Task DeleteAsync(Guid id)
       {
           await this._repository.DeleteAsync(id);
       }

       public async Task<IEnumerable<ScoreScale>> GetAllAsync()
       {
           return await this._repository.GetAllAsync();
       }

       public async Task<ScoreScale> GetByIdAsync(Guid id)
       {
           var scale = await this._repository.GetByIdAsync(id);
           if (scale == null) throw new KeyNotFoundException(nameof(scale));
           return scale;
       }

       public async Task<Guid> InsertAsync(ScoreScale entity)
       {
           return await this._repository.InsertAsync(entity);
       }

       public async Task UpdateAsync(Guid id, ScoreScale entity)
       {
           await this._repository.UpdateAsync(id, entity);
       }
   }
}