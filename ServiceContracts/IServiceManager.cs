namespace ServiceContracts
{
    public interface IServiceManager
    {
        ICategoryService CategoryService { get; }
        IUserService UserService { get; }
        IProductService ProductService { get; }
        IBasketService BasketService { get; }
    }
}
