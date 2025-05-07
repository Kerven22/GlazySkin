using Entity.Models;

namespace RepositoryContracts
{
    public interface IProducRepository
    {
        IEnumerable<Product> GetProducts(Guid categoryId, bool trackChanges);

        Product GetProduct(Guid categoryId, Guid productId, bool trackChanges);
    }
}
