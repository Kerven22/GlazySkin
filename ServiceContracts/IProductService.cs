using Entity.Models;
using Shared;

namespace ServiceContracts
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts(Guid categoryId, bool trackChanges);

        ProductDto GetProduct(Guid categoryId, Guid productId, bool trackChanges);

        ProductDto CreateProduct(Guid categoryId, ProductForCreationDto product);

    }
}
