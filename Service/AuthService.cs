using AutoMapper;
using Companies.Shared.DTOs;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service;
public class AuthService : IAuthService
{

    private readonly IMapper mapper;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration configuration;
    private ApplicationUser? user;

    public AuthService(IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.configuration = configuration;
    }

    public async Task<string> CreateTokenAsync()
    {
        SigningCredentials signing = GetSigningCredentials();
        IEnumerable<Claim> claims = await GetClaimsAsync();
        JwtSecurityToken tokenOptions = GenerateTokenOptions(signing, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signing, IEnumerable<Claim> claims)
    {
         var jwtSettings = configuration.GetSection("JwtSettings");

        var tokenOptions = new JwtSecurityToken(
                                    issuer: jwtSettings["Issuer"],
                                    audience: jwtSettings["Audience"],
                                    claims: claims,
                                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["Expires"])),
                                    signingCredentials: signing);

        return tokenOptions;
    }

    private async Task<IEnumerable<Claim>> GetClaimsAsync()
    {
        ArgumentNullException.ThrowIfNull(user);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim("Age", user.Age.ToString()!),
            new Claim(ClaimTypes.NameIdentifier, user.Id!),
            //Add more if needed
        };

        var roles = await userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var secretKey = configuration["secretkey"];
        ArgumentNullException.ThrowIfNull(secretKey, nameof(secretKey));

        var key = Encoding.UTF8.GetBytes(secretKey);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

    }

    public async Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistration)
    {
        ArgumentNullException.ThrowIfNull(userForRegistration, nameof(userForRegistration));

        var roleExists = await roleManager.RoleExistsAsync(userForRegistration.Role!);
        if (!roleExists)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Role does not exist" });
        }

        var user = mapper.Map<ApplicationUser>(userForRegistration);

        //ToDo: Check if company exists!!!!
        var result = await userManager.CreateAsync(user, userForRegistration.Password!);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, userForRegistration.Role!);
        }

        return result;
    }

    public async Task<bool> ValidateUserAsync(UserForAuthDto userDto)
    {
          ArgumentNullException.ThrowIfNull(userDto, nameof(userDto));

          user = await userManager.FindByNameAsync(userDto.UserName!);

          return user != null && await userManager.CheckPasswordAsync(user, userDto.Password!);

    }
}

