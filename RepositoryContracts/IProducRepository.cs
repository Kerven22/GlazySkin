using Entity.Models;
using Shared;

namespace RepositoryContracts
{
    public interface IProducRepository
    {
        IEnumerable<Product> GetProducts(Guid categoryId, bool trackChanges);

        Product GetProduct(Guid categoryId, Guid productId, bool trackChanges);

        void CreateProduct(Guid categoryId, Product product);

        void DeleteProduct(Product product); 
    }
}
