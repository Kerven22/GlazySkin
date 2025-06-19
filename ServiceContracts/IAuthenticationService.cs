using Microsoft.AspNetCore.Identity;
using Shared;

namespace ServiceContracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto registrationDto);
        Task<bool> ValidateUser(UserForAuthenticationDto user);
        Task<TokenDto> CreateToken(); 
    }
}
