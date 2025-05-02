using RepositoryContracts;
using ServiceContracts;

namespace Servicies
{
    internal sealed class ProductService(IRepositoryManager _repositoryManager):IProductService
    {
    }
}
