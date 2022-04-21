using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

namespace Website.Backend.Controllers
{
    /// <summary>
    /// Responsible for issuing JWT tokens
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly ILoginService _loginService;

        public LoginController(IConfiguration config, ILoginService loginService)
        {
            _config = config;

            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginCredentials login)
        {
            IActionResult response = Unauthorized();

            User user = await _loginService.AuthenticateUserCredentials(login);

            if (user != null)
            {
                var tokenString = await _loginService.GenerateJsonWebToken(user);
                
                response = Ok(new { token = tokenString });
            }

            return response;
        }
    }
}
