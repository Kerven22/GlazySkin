using RepositoryContracts;
using ServiceContracts;

namespace Servicies
{
    internal sealed class UserService(IRepositoryManager _repositoryManager):IUserService
    {
    }
}
