using Entity;
using Entity.Models;
using RepositoryContracts;

namespace Repositories
{
    public class BasketRepository:RepositoryBase<Basket>, IBasketRepository
    {
        public BasketRepository(GlazySkinDbContext glazySkinDbContext) : base(glazySkinDbContext) { }
    }
}
