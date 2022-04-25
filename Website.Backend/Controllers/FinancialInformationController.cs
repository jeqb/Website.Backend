using Microsoft.AspNetCore.Mvc;
using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

namespace Website.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialInformationController : ControllerBase
    {
        private readonly ILogger<FinancialInformationController> _logger;

        private readonly IFinancialService _financialService;

        public FinancialInformationController(ILogger<FinancialInformationController> logger,
            IFinancialService financialService)
        {
            _logger = logger;

            _financialService = financialService;
        }

        // GET: api/<FinancialInformationController>
        [HttpGet]
        [ProducesResponseType(typeof(FinancialInformationModel), 200)]
        public async Task<IActionResult> Get()
        {
            FinancialInformationModel info = await _financialService.GetFinancialInformationAsync();

            return Ok(info);
        }
    }
}
