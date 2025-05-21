using Entity;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.RepositoryExtentions;
using RepositoryContracts;
using Shared;
using Shared.RequestFeatures;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProducRepository
    {
        public ProductRepository(GlazySkinDbContext glazySkinDbContext) : base(glazySkinDbContext) { }

        public async Task<PagedList<Product>> GetProductsAsync(Guid categoryId, ProductParameters productParameters,
            bool trackChanges)
        {
            var products = await FindByCondition(p => p.CategoryId.Equals(categoryId), trackChanges)
                .FilterProducts(productParameters.MaxCost, productParameters.MinCost)
                .Search(productParameters.SearchTerm)
                .Skip((productParameters.PageNumber-1)*productParameters.PageSize)
                .Take(productParameters.PageSize)
                .Sorting(productParameters.OrderBy)
                .ToListAsync();

            var count = await FindByCondition(p => p.CategoryId.Equals(categoryId), trackChanges).CountAsync(); 
            return new PagedList<Product>(products, count, productParameters.PageNumber, productParameters.PageSize); 
        }

        public async Task<Product> GetProductAsync(Guid categoryId, Guid productId, bool trackChanges) =>
            await FindByCondition(p => p.CategoryId.Equals(categoryId) && p.ProductId.Equals(productId), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateProduct(Guid categoryId, Product product)
        {
            product.CategoryId = categoryId;
            Create(product); 
        }

        public void DeleteProduct(Product product) => Delete(product); 
    }
}
