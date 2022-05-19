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
        private readonly ILogger<MessageController> _logger;

        private readonly IMessageService _messageService;
        
        public MessageController(ILogger<MessageController> logger, IMessageService messageService)
        {
            _logger = logger;

            _messageService = messageService;
        }

        // GET: api/<MessageController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MessageModel>), 200)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<MessageModel> messages = await _messageService.GetAll();

            return Ok(messages);
        }

        // GET api/<MessageController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MessageModel), 200)]
        public async Task<IActionResult> Get(string id)
        {
            MessageModel? message = await _messageService.GetById(id);

            if (message != null)
            {
                return Ok(message);
            }
            else
            {
                return NotFound(message);
            }
        }

        // POST api/<MessageController>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(MessageModel), 201)]
        public async Task<IActionResult> Post([FromBody] MessageModel message)
        {
            MessageModel createdMessage = await _messageService.Create(message);

            return Created("api/Message", createdMessage);
        }


        // PUT api/<MessageController>
        [HttpPut]
        [ProducesResponseType(typeof(MessageModel), 204)]
        public async Task<IActionResult> Update(MessageModel message)
        {
            MessageModel updatedMessage = await _messageService.Update(message);

            return Ok(updatedMessage);
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete(string id)
        {
            await _messageService.Delete(id);

            return Ok();
        }
    }
}
