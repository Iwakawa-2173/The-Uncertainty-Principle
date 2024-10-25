using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TUP.WebApi.Controllers
{
    [ApiController]
    [Route("responses")]
    public class ResponsesController : ControllerBase
    {
        private readonly ILogger<ResponsesController> logger;
        private readonly IRepository<Response> responsesRepository;

        public ResponsesController(ILogger<ResponsesController> logger, IRepository<Response> responsesRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.responsesRepository = responsesRepository ?? throw new ArgumentNullException(nameof(responsesRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Response>>> Get()
        {
            this.logger.LogInformation("get all responses");
            return Ok(await responsesRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> Get(int id)
        {
            var response = await responsesRepository.GetByIdAsync(id);
            if (response == null) return NotFound();
            
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] Response response)
        {
            if (response.PlayerId <= 0 || response.EventId <= 0 || response.ResponseOption < 1 || response.ResponseOption > 3)
                return BadRequest("Некорректные данные ответа!");

            return Ok(await responsesRepository.InsertAsync(response));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] Response response)
        {
            var existingResponse = await responsesRepository.GetByIdAsync(id);
            if (existingResponse == null) return NotFound();

            existingResponse.ResponseOption = response.ResponseOption;
            
            await responsesRepository.UpdateAsync(id, existingResponse);
            
            return Ok();
       }

       [HttpDelete("{id}")]
       public async Task<ActionResult> Delete([FromRoute] int id)
       {
           var existingResponse = await responsesRepository.GetByIdAsync(id);
           if (existingResponse == null) return NotFound();

           await responsesRepository.DeleteAsync(id);
           return Ok();
       }
   }
}