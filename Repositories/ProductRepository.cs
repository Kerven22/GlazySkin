using Entity;
using Entity.Models;
using RepositoryContracts;

namespace Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProducRepository
    {
        public ProductRepository(GlazySkinDbContext glazySkinDbContext) : base(glazySkinDbContext) { }
    }
}
