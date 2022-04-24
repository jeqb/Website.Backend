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
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginCredentialsModel login)
        {
            IActionResult response = Unauthorized();

            UserModel user = await _loginService.AuthenticateUserCredentials(login);

            if (user != null)
            {
                var tokenString = await _loginService.GenerateJsonWebToken(user);
                
                response = Ok(new { token = tokenString });
            }

            return response;
        }
    }
}
