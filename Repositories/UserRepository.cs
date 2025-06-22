using Entity;
using Entity.Models;
using Microsoft.AspNetCore.Identity;
using RepositoryContracts;
using Shared;

namespace Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IBasketRepository _basketRepository; 

        public UserRepository(GlazySkinDbContext glazySkinDbContext, UserManager<User> userManager) : base(glazySkinDbContext)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Register(User user, UserForRegistrationDto registrationDto)
        {
            var result = await _userManager.CreateAsync(user, registrationDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, registrationDto.Roles);
            }

            return result; 
        }
    }
}
