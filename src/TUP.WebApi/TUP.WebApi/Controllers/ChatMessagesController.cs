using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TUP.WebApi.Controllers
{
   [ApiController]
   [Route("chat-messages")]
   public class ChatMessagesController : ControllerBase
   {
       private readonly ILogger<ChatMessagesController> logger;
       private readonly IRepository<ChatMessage> chatMessagesRepository;

       public ChatMessagesController(ILogger<ChatMessagesController> logger, IRepository<ChatMessage> chatMessagesRepository)
       {
           this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
           this.chatMessagesRepository = chatMessagesRepository ?? throw new ArgumentNullException(nameof(chatMessagesRepository));
       }

       [HttpGet]
       public async Task<ActionResult<IEnumerable<ChatMessage>>> Get()
       {
           this.logger.LogInformation("get all chat messages");
           return Ok(await chatMessagesRepository.GetAllAsync());
       }

       [HttpGet("{id}")]
       public async Task<ActionResult<ChatMessage>> Get(Guid id)
       {
           var message = await chatMessagesRepository.GetByIdAsync(id);
           if (message == null) return NotFound();
           
           return Ok(message);
       }

       [HttpPost]
       public async Task<ActionResult> Insert([FromBody] ChatMessage message)
       {
           if (string.IsNullOrWhiteSpace(message.Message))
               return BadRequest("Сообщение не может быть пустым!");

           return Ok(await chatMessagesRepository.InsertAsync(message));
       }

       [HttpPut("{id}")]
       public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] ChatMessage message)
       {
           var existingMessage = await chatMessagesRepository.GetByIdAsync(id);
           if (existingMessage == null) return NotFound();

           existingMessage.Message = message.Message;

           await chatMessagesRepository.UpdateAsync(id, existingMessage);

           return Ok();
      }

      [HttpDelete("{id}")]
      public async Task<ActionResult> Delete([FromRoute] Guid id)
      {
          var existingMessage = await chatMessagesRepository.GetByIdAsync(id);
          if (existingMessage == null) return NotFound();

          await chatMessagesRepository.DeleteAsync(id);
          return Ok();
      }
   }
}