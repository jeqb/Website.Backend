using Microsoft.AspNetCore.Mvc;
using Website.Backend.Models;

namespace Website.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        [ProducesResponseType(typeof(FinancialInformationModel), 200)]
        public async Task<IActionResult> Get()
        {
            FinancialInformationModel model = new()
            {
                BitcoinPrice = 234,
                GoldSpotPrice = 432,
                Sp500PriceChangePercent = 513513,
            };

            return Ok(model);
        }
    }
}
