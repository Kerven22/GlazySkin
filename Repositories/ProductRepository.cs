using Entity;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using Shared;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProducRepository
    {
        public ProductRepository(GlazySkinDbContext glazySkinDbContext) : base(glazySkinDbContext) { }

        public async Task<IEnumerable<Product>> GetProductsAsync(Guid categoryId, bool trackChanges) =>
            await FindByCondition(p => p.CategoryId.Equals(categoryId), trackChanges)
                .OrderBy(p => p.Name).ToListAsync();

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
