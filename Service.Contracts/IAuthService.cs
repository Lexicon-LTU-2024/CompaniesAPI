using Companies.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IAuthService
    {
        Task<TokenDto> CreateTokenAsync(bool expireTime);
        Task<TokenDto> RefreshTokenAsync(TokenDto token);
        Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistration);
        Task<bool> ValidateUserAsync(UserForAuthDto user);
    }
}
