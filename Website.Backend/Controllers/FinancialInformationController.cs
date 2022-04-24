using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website.Backend.Models;

namespace Website.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialInformationController : ControllerBase
    {
        public FinancialInformationController()
        {

        }

        // GET: api/<FinancialInformationController>
        [HttpGet]
        [ProducesResponseType(typeof(FinancialInformationModel), 200)]
        public async Task<IActionResult> Get()
        {
            // TODO: due to api rate limiting, probably need to make a background
            // process that updates a table every N minutes. Then there is a service
            // that reads the LATEST results from that/those tables.
            FinancialInformationModel info = new();

            return Ok(info);
        }
    }
}
