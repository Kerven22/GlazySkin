using Entity.Models;
using Shared;
using Shared.RequestFeatures;

namespace RepositoryContracts
{
    public interface IProducRepository
    {
        Task<PagedList<Product>> GetProductsAsync(Guid categoryId, ProductParameters productParameters, bool trackChanges);

        Task<Product> GetProductAsync(Guid categoryId, Guid productId, bool trackChanges);

        void CreateProduct(Guid categoryId, Product product);

        void DeleteProduct(Product product); 
    }
}
