using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Backend.Domain;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.Models;
using System.Linq;
using Website.Backend.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Website.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IRepository<MessageDomain> _repository;
        
        public MessageController(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.CreateMessageRepository();
        }

        // GET: api/<MessageController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Message>), 200)]
        public async Task<IActionResult> Get()
        {
            IEnumerable<MessageDomain> messages = await _repository.GetAll();

            IEnumerable<Message> response = messages
                                            .Select((message) => message.ToModel()
                                            );

            return Ok(response);
        }

        // GET api/<MessageController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Message), 200)]
        public async Task<IActionResult> Get(int id)
        {
            MessageDomain message = await _repository.GetById(id);

            return Ok(message.ToModel());
        }

        // POST api/<MessageController>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Message), 201)]
        public async Task<IActionResult> Post([FromBody] Message message)
        {
            MessageDomain createdMessage = await _repository.Create(message.ToDomain());

            return Created("api/Message", createdMessage.ToModel());
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);

            return Ok();
        }
    }
}
