using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace The_Uncertainty_Principle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
       private readonly GameDbContext _context;

       public ChatMessagesController(GameDbContext context)
       {
           _context = context;
       }

       [HttpGet]
       public ActionResult<IEnumerable<ChatMessage>> GetChatMessages()
       {
           return _context.ChatMessages.ToList();
       }

       [HttpGet("{id}")]
       public ActionResult<ChatMessage> GetChatMessage(int id)
       {
           var chatMessage = _context.ChatMessages.Find(id);
           if (chatMessage == null) return BadRequest();
           return chatMessage;
       }

       [HttpPost]
       public ActionResult<ChatMessage> CreateChatMessage(ChatMessage chatMessage)
       {
           _context.ChatMessages.Add(chatMessage);
           _context.SaveChanges();
           return CreatedAtAction(nameof(GetChatMessage), new { id = chatMessage.MessageId }, chatMessage);
       }

       [HttpPut("{id}")]
       public IActionResult UpdateChatMessage(int id, ChatMessage chatMessage)
       {
           if (id != chatMessage.MessageId) return BadRequest();
           _context.Entry(chatMessage).State = EntityState.Modified;
           _context.SaveChanges();
           return NoContent();
       }

       [HttpDelete("{id}")]
       public IActionResult DeleteChatMessage(int id)
       {
           var chatMessage = _context.ChatMessages.Find(id);
           if (chatMessage == null) return BadRequest();
           _context.ChatMessages.Remove(chatMessage);
           _context.SaveChanges();
           return NoContent();
      }
   }
}
