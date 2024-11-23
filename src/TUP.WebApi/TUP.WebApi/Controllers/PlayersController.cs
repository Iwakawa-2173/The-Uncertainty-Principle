using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TUP.WebApi.Controllers
{
    [ApiController]
    [Route("players")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> logger;
        private readonly IRepository<Player> playersRepository;

        public PlayersController(ILogger<PlayersController> logger, IRepository<Player> playersRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.playersRepository = playersRepository ?? throw new ArgumentNullException(nameof(playersRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> Get()
        {
            this.logger.LogInformation("get all players");
            return Ok(await playersRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> Get(Guid id)
        {
            var player = await playersRepository.GetByIdAsync(id);
            if (player == null) return NotFound();
            return Ok(player);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] Player player)
        {
            if (string.IsNullOrWhiteSpace(player.UniquePlayerName))
                return BadRequest("Укажите уникальное имя игрока!");

            return Ok(await playersRepository.InsertAsync(player));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] Player player)
        {
            var existingPlayer = await playersRepository.GetByIdAsync(id);
            if (existingPlayer == null) return NotFound();

            existingPlayer.UniquePlayerName = player.UniquePlayerName;
            await playersRepository.UpdateAsync(id, existingPlayer);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var existingPlayer = await playersRepository.GetByIdAsync(id);
            if (existingPlayer == null) return NotFound();

            await playersRepository.DeleteAsync(id);
            return Ok();
        }
    }
}