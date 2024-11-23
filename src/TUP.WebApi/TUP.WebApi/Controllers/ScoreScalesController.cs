using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TUP.WebApi.Controllers
{
   [ApiController]
   [Route("score-scales")]
   public class ScoreScalesController : ControllerBase
   {
       private readonly ILogger<ScoreScalesController> logger;
       private readonly IRepository<ScoreScale> scoreScalesRepository;

       public ScoreScalesController(ILogger<ScoreScalesController> logger, IRepository<ScoreScale> scoreScalesRepository)
       {
           this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
           this.scoreScalesRepository = scoreScalesRepository ?? throw new ArgumentNullException(nameof(scoreScalesRepository));
       }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<ScoreScale>>> Get()
       {
           this.logger.LogInformation("get all score scales");
           return Ok(await scoreScalesRepository.GetAllAsync());
       }

       [HttpGet("{id}")]
       public async Task<ActionResult<ScoreScale>> Get(Guid id)
       {
           var scale = await scoreScalesRepository.GetByIdAsync(id);
           if (scale == null) return NotFound();
           
           return Ok(scale);
       }

       [HttpPost]
       public async Task<ActionResult> Insert([FromBody] ScoreScale scale)
       {
           scale.Scale1 = 0; // Инициализация очков
           scale.Scale2 = 0;
           scale.Scale3 = 0;

           return Ok(await scoreScalesRepository.InsertAsync(scale));
       }

       [HttpPut("{id}")]
       public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] ScoreScale scale)
       {
           var existingScale = await scoreScalesRepository.GetByIdAsync(id);
           if (existingScale == null) return NotFound();

           existingScale.Scale1 += scale.Scale1;
           existingScale.Scale2 += scale.Scale2;
           existingScale.Scale3 += scale.Scale3;

           await scoreScalesRepository.UpdateAsync(id, existingScale);

           return Ok();
       }

       [HttpDelete("{id}")]
       public async Task<ActionResult> Delete([FromRoute] Guid id)
       {
          var existingScale = await scoreScalesRepository.GetByIdAsync(id);
          if (existingScale == null) return NotFound();

          await scoreScalesRepository.DeleteAsync(id);
          return Ok();
      }
   }
}