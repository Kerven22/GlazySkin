using AutoMapper;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;

namespace Servicies
{
    public class ServiceManager:IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly Lazy<ICategoryService> _categoryService; 
        private readonly Lazy<IUserService> _userService;         
        private readonly Lazy<IProductService> _productService;         
        private readonly Lazy<IBasketService> _basketService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper _mapper)
        {
            _repositoryManager = repositoryManager;
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(_repositoryManager, _mapper));
            _userService = new Lazy<IUserService>(() => new UserService(_repositoryManager));
            _productService = new Lazy<IProductService>(() => new ProductService(_repositoryManager, _mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(_repositoryManager));
        }

        public IUserService UserService => _userService.Value;
        public ICategoryService CategoryService => _categoryService.Value;
        public IProductService ProductService => _productService.Value;
        public IBasketService BasketService => _basketService.Value; 
    }
}
