using Entity.Models;

namespace RepositoryContracts
{
    public interface IBasketRepository
    {
        Task<Basket> CreateBasket(string UserId, string basketId);

        Task DeleteBasket(Guid userId, Guid BasketId);

        Task UpdatePartiallyBasket(Guid userId, Guid BasketId);
    }
}
