using Entity;
using Entity.Models;
using RepositoryContracts;

namespace Repositories
{
    public class BasketRepository:RepositoryBase<Basket>, IBasketRepository
    {
        public BasketRepository(GlazySkinDbContext glazySkinDbContext) : base(glazySkinDbContext) { }

        public async Task<Basket> CreateBasket(string UserId, string basketId)
        {
            var basket = new Basket { BasketId = basketId, Id = UserId };
            await Create(basket);
            await _dbContext.SaveChangesAsync();  
            return basket; 
        }

        public Task DeleteBasket(Guid userId, Guid BasketId)
        {
            throw new NotImplementedException();
        }


        public Task UpdatePartiallyBasket(Guid userId, Guid BasketId)
        {
            throw new NotImplementedException();
        }
    }
}