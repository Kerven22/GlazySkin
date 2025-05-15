using Entity.Models;
using Shared;

namespace RepositoryContracts
{
    public interface IProducRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(Guid categoryId, bool trackChanges);

        Task<Product> GetProductAsync(Guid categoryId, Guid productId, bool trackChanges);

        void CreateProduct(Guid categoryId, Product product);

        void DeleteProduct(Product product); 
    }
}
