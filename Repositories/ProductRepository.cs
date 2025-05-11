using Entity;
using Entity.Models;
using RepositoryContracts;
using Shared;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProducRepository
    {
        public ProductRepository(GlazySkinDbContext glazySkinDbContext) : base(glazySkinDbContext) { }

        public IEnumerable<Product> GetProducts(Guid categoryId, bool trackChanges) =>
            FindByCondition(p => p.CategoryId.Equals(categoryId), trackChanges)
                .OrderBy(p => p.Name).ToList();

        public Product GetProduct(Guid categoryId, Guid productid, bool trackChanges) =>
            FindByCondition(p => p.CategoryId.Equals(categoryId) && p.ProductId.Equals(productid), trackChanges)
                .SingleOrDefault();

        public void CreateProduct(Guid categoryId, Product product)
        {
            product.CategoryId = categoryId;
            Create(product); 
        }

        public void DeleteProduct(Product product) => Delete(product); 
    }
}
