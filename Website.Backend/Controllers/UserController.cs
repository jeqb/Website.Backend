using Microsoft.AspNetCore.Mvc;
using Website.Backend.Models;
using Website.Backend.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Website.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _repository;

        public UserController(IRepositoryFactory repositoryFactory)
        {
            _repository = repositoryFactory.CreateUserRepository();
        }
        
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _repository.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await _repository.GetById(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<User> Post([FromBody] User user)
        {
            return await _repository.Create(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<User> Put([FromBody] User user)
        {
            return await _repository.Update(user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
