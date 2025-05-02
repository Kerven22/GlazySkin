using Entity;
using Entity.Models;
using RepositoryContracts;

namespace Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(GlazySkinDbContext glazySkinDbContext) : base(glazySkinDbContext) { }
    }
}
