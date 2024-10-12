using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace The_Uncertainty_Principle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsesController : ControllerBase, IResponsesController // Реализация интерфейса
    {
      private readonly GameDbContext _context;

      public ResponsesController(GameDbContext context)
      {
          _context = context;
      }

      [HttpGet]
      public ActionResult<IEnumerable<Response>> GetResponses()
      {
          return _context.Responses.ToList();
      }

      [HttpGet("{id}")]
      public ActionResult<Response> GetResponse(int id)
      {
          var response = _context.Responses.Find(id);
          if (response == null) return BadRequest();
          return response;
      }

      [HttpPost]
      public ActionResult<Response> CreateResponse(Response response)
      {
          _context.Responses.Add(response);
          _context.SaveChanges();
          return CreatedAtAction(nameof(GetResponse), new { id = response.ResponseId }, response);
      }

      [HttpPut("{id}")]
      public IActionResult UpdateResponse(int id, Response response)
      {
          if (id != response.ResponseId) return BadRequest();
          _context.Entry(response).State = EntityState.Modified;
          _context.SaveChanges();
          return NoContent();
      }

      [HttpDelete("{id}")]
      public IActionResult DeleteResponse(int id)
      {
          var response = _context.Responses.Find(id);
          if (response == null) return BadRequest();
          _context.Responses.Remove(response);
          _context.SaveChanges();
          return NoContent();
      }
   }
}
