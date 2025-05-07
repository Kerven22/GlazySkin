using AutoMapper;
using RepositoryContracts;
using ServiceContracts;
using Servicies.Exceptions;
using Shared;

namespace Servicies
{
    internal sealed class ProductService(IRepositoryManager _repositoryManager, IMapper _mapper):IProductService
    {

        private void CategoryIsintExists(Guid categoryId)
        {
            var category = _repositoryManager.CategoryRepository.GetCategoryById(categoryId, trackChanges:false);
            if (category is null)
                throw new CategoryNotFoundException(categoryId); 
        }
        public IEnumerable<ProductDto> GetProducts(Guid categoryId, bool trackChanges)
        {
            CategoryIsintExists(categoryId); 
            var productsEntity =_repositoryManager.ProducRepository.GetProducts(categoryId, trackChanges);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(productsEntity);

            return productsDto; 
        }

        public ProductDto GetProduct(Guid categoryId, Guid productId, bool trackChanges)
        {
            CategoryIsintExists(categoryId);

            var productEntity = _repositoryManager.ProducRepository.GetProduct(categoryId, productId, trackChanges);

            if (productEntity is null)
                throw new ProductNotFoundException(productId);
            var productDto = _mapper.Map<ProductDto>(productEntity);
            return productDto; 
        }
    }
}
