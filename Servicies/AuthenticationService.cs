using AutoMapper;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using ServiceContracts;
using Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Servicies
{
    public class AuthenticationService(
        IMapper _mapper,
        IConfiguration configuration,
        UserManager<User> _userManager) : IAuthenticationService
    {

        private User? _user;

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto registrationDto)
        {
            var user = _mapper.Map<User>(registrationDto);

            var result = await _userManager.CreateAsync(user, registrationDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, registrationDto.Roles);

            return result;
        }

        public async Task<TokenDto> CreateToken()
        {
            var signingCredentials = GetSingnigCredentials();

            var claims = GetClaims();

            var tokenOptions = GenerateTokenOptions(signingCredentials, claims.Result);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto user)
        {
            _user = await _userManager.FindByNameAsync(user.UserName);

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, user.Password));

            if (!result)
                Log.Warning($"{nameof(ValidateUser)}: Authentication failed. Wrong user Name or Password");
            return result;
        }

        private SigningCredentials GetSingnigCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audince"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiers"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32]; 
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber); 
            }
        }
    }
}
