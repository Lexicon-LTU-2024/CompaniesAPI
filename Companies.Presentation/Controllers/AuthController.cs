using Companies.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthControlller : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthControlller(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var result = await _serviceManager.AuthService.RegisterUserAsync(userForRegistration);
            return result.Succeeded ? StatusCode(StatusCodes.Status201Created) : BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(UserForAuthDto user)
        {
            if (!await _serviceManager.AuthService.ValidateUserAsync(user))
                return Unauthorized();

            return Ok( new { Token = await _serviceManager.AuthService.CreateTokenAsync() });
        }
    }
}
