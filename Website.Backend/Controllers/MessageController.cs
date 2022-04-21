using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Website.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET: api/<MessageController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Message>), 200)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Message> messages = await _messageService.GetAll();

            return Ok(messages);
        }

        // GET api/<MessageController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Message), 200)]
        public async Task<IActionResult> Get(int id)
        {
            Message message = await _messageService.GetById(id);

            return Ok(message);
        }

        // POST api/<MessageController>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Message), 201)]
        public async Task<IActionResult> Post([FromBody] Message message)
        {
            Message createdMessage = await _messageService.Create(message);

            return Created("api/Message", createdMessage);
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete(int id)
        {
            await _messageService.Delete(id);

            return Ok();
        }
    }
}
