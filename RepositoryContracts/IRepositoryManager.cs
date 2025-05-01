namespace RepositoryContracts
{
    public interface IRepositoryManager
    {
        ICategoryRepository CategoryRepository { get; }
        IUserRepository UserRepository { get; }
        IProducRepository ProducRepository { get; }
        IBasketRepository BasketRepository { get; }

        Task SaveAsync();
    }
}
