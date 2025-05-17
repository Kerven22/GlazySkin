using Entity.Models;
using Shared;
using Shared.RequestFeatures;

namespace ServiceContracts
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductDto> productDtos, MetaData metaData)> GetProductsAsync(Guid categoryId, ProductParameters productParameters, bool trackChanges);

        
        Task<ProductDto> GetProductAsync(Guid categoryId, Guid productId, bool trackChanges);

        Task<ProductDto> CreateProductAsync(Guid categoryId, ProductForCreationDto product);

        Task DeleteProductAsync(Guid categoryId, Guid productId, bool trackChanges);

        Task ProductUpdateAsync(Guid categoryId, Guid productId, ProductForUpdateDto productForUpdateDto,
            bool catTrackChanges, bool productTrackChanges);


        Task<(ProductForUpdateDto productForUpdateDto, Product product)> GetProductForPatchAsync(Guid categoryId, Guid productId,
            bool categoryTrachChanges, bool productTrackChanges);

        Task SaveChangesForPatchAsync(ProductForUpdateDto productForUpdateDto, Product product); 

    }
}
