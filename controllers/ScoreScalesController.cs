using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace The_Uncertainty_Principle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreScalesController : ControllerBase
    {
       private readonly GameDbContext _context;

       public ScoreScalesController(GameDbContext context)
       {
           _context = context;
       }

       [HttpGet]
       public ActionResult<IEnumerable<ScoreScale>> GetScoreScales()
       {
           return _context.ScoreScales.ToList();
       }

       [HttpGet("{id}")]
       public ActionResult<ScoreScale> GetScoreScale(int id)
       {
           var scoreScale = _context.ScoreScales.Find(id);
           if (scoreScale == null) return NotFound();
           return scoreScale;
       }

       [HttpPost]
       public ActionResult<ScoreScale> CreateScoreScale(ScoreScale scoreScale)
       {
           _context.ScoreScales.Add(scoreScale);
           _context.SaveChanges();
           return CreatedAtAction(nameof(GetScoreScale), new { id = scoreScale.ScaleId }, scoreScale);
       }

       [HttpPut("{id}")]
       public IActionResult UpdateScoreScale(int id, ScoreScale scoreScale)
       {
           if (id != scoreScale.ScaleId) return BadRequest();
           _context.Entry(scoreScale).State = EntityState.Modified;
           _context.SaveChanges();
           return NoContent();
       }

       [HttpDelete("{id}")]
       public IActionResult DeleteScoreScale(int id)
       {
           var scoreScale = _context.ScoreScales.Find(id);
           if (scoreScale == null) return NotFound();
           _context.ScoreScales.Remove(scoreScale);
           _context.SaveChanges();
           return NoContent();
       }
   }
}