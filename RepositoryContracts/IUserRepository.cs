using Entity.Models;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace RepositoryContracts
{
    public interface IUserRepository
    {
        Task<IdentityResult> Register(User user, UserForRegistrationDto registrationDto); 
    }
}
