using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Backend.Models;
using Website.Backend.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Website.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IRepository<Message> _repository;
        
        public MessageController(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.CreateMessageRepository();
        }

        // GET: api/<MessageController>
        [HttpGet]
        public async Task<IEnumerable<Message>> Get()
        {
            return await _repository.GetAll();
        }

        // GET api/<MessageController>/5
        [HttpGet("{id}")]
        public async Task<Message> Get(int id)
        {
            return await _repository.GetById(id);
        }

        // POST api/<MessageController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Message> Post([FromBody] Message message)
        {
            return await _repository.Create(message);
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
