using AutoMapper;
using Entity.Models;
using RepositoryContracts;
using ServiceContracts;
using Servicies.Exceptions;
using Shared;
using Shared.RequestFeatures;

namespace Servicies
{
    internal sealed class ProductService(IRepositoryManager _repositoryManager, IMapper _mapper):IProductService
    {
        private async Task CheckIfCategoryExistsAsync(Guid categoryId)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(categoryId, trackChanges:false);
            if (category is null)
                throw new CategoryNotFoundException(categoryId); 
        }
        public async Task<(IEnumerable<ProductDto> productDtos, MetaData metaData)> GetProductsAsync(Guid categoryId,ProductParameters productParameters, bool trackChanges)
        {
            await CheckIfCategoryExistsAsync(categoryId);
            var produtcWithMetaData =
                await _repositoryManager.ProducRepository.GetProductsAsync(categoryId, productParameters, trackChanges);
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(produtcWithMetaData);
            return (productDtos: productDto, metaData: produtcWithMetaData.MetaData); 
        }

        public async Task<ProductDto> GetProductAsync(Guid categoryId, Guid productId, bool trackChanges)
        {
            await CheckIfCategoryExistsAsync(categoryId);
            var productEntity = await _repositoryManager.ProducRepository.GetProductAsync(categoryId, productId, trackChanges);

            if (productEntity is null)
                throw new ProductNotFoundException(productId);
            var productDto = _mapper.Map<ProductDto>(productEntity);
            return productDto; 
        }

        public async Task<ProductDto> CreateProductAsync(Guid categoryId, ProductForCreationDto product)
        {
            await CheckIfCategoryExistsAsync(categoryId);

            var productEntity = _mapper.Map<Product>(product);
            
            productEntity.ProductId = Guid.NewGuid();
            
            _repositoryManager.ProducRepository.CreateProduct(categoryId, productEntity);
            await _repositoryManager.SaveAsync();

            var productDto = _mapper.Map<ProductDto>(productEntity);
            return productDto; 
        }

        public async Task DeleteProductAsync(Guid categoryId, Guid productId, bool trackChanges)
        {
            await CheckIfCategoryExistsAsync(categoryId);

            var product = await _repositoryManager.ProducRepository.GetProductAsync(categoryId, productId, trackChanges);
            if (product is null)
                throw new ProductNotFoundException(productId); 
            _repositoryManager.ProducRepository.DeleteProduct(product);
            await _repositoryManager.SaveAsync();
        }
        
        public async Task ProductUpdateAsync(Guid categoryId, Guid productId, ProductForUpdateDto productForUpdateDto, bool catTrackChanges,
            bool productTrackChanges)
        {
            var category = _repositoryManager.CategoryRepository.GetCategoryByIdAsync(categoryId, catTrackChanges);
            if (category is null)
                throw new CategoryNotFoundException(categoryId);

            var product = await _repositoryManager.ProducRepository.GetProductAsync(categoryId, productId, productTrackChanges);
            if (product is null)
                throw new ProductNotFoundException(productId);

            _mapper.Map(productForUpdateDto,product);
            await _repositoryManager.SaveAsync();
        }

        public async Task<(ProductForUpdateDto productForUpdateDto, Product product)> GetProductForPatchAsync(Guid categoryId, Guid productId,
            bool categoryTrachChanges, bool productTrackChanges)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(categoryId, categoryTrachChanges);
            if (category is null)
                throw new CategoryNotFoundException(categoryId);
            var product = await _repositoryManager.ProducRepository.GetProductAsync(categoryId, productId, productTrackChanges);
            if (product is null)
                throw new ProductNotFoundException(productId);

            var productToPatch = _mapper.Map<ProductForUpdateDto>(product);
            return (productToPatch, product); 
        }
        public async Task SaveChangesForPatchAsync(ProductForUpdateDto productForUpdateDto, Product product)
        {
            _mapper.Map(productForUpdateDto, product);
            await _repositoryManager.SaveAsync(); 
        }
    }
}
