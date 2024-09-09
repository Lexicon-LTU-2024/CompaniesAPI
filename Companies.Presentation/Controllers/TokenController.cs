using Companies.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Presentation.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController(IAuthService authenticationService) : ControllerBase
{
    [HttpPost("refresh")]
    public async Task<ActionResult<TokenDto>> RefreshToken(TokenDto token)
    {
        TokenDto tokenDto = await authenticationService.RefreshTokenAsync(token);

        return Ok(tokenDto);
    }
}
