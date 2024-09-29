using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace The_Uncertainty_Principle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly GameDbContext _context;

        public PlayersController(GameDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetPlayers()
        {
            return _context.Players.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Player> GetPlayer(int id)
        {
            var player = _context.Players.Find(id);
            if (player == null) return NotFound();
            return player;
        }

        [HttpPost]
        public ActionResult<Player> CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPlayer), new { id = player.PlayerId }, player);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, Player player)
        {
            if (id != player.PlayerId) return BadRequest();
            _context.Entry(player).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            var player = _context.Players.Find(id);
            if (player == null) return NotFound();
            _context.Players.Remove(player);
            _context.SaveChanges();
            return NoContent();
        }
    }
}