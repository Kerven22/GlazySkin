using Entity;
using RepositoryContracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly GlazySkinDbContext _dbContext;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<IProducRepository> _productRepository;
        private readonly Lazy<IBasketRepository> _basketRepository;

        public RepositoryManager(GlazySkinDbContext glazySkinDbContext)
        {
            _dbContext = glazySkinDbContext;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_dbContext));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_dbContext));
            _productRepository = new Lazy<IProducRepository>(() => new ProductRepository(_dbContext));
            _basketRepository = new Lazy<IBasketRepository>(() => new BasketRepository(_dbContext));
        }

        public ICategoryRepository CategoryRepository => _categoryRepository.Value;

        public IUserRepository UserRepository => _userRepository.Value;

        public IProducRepository ProducRepository => _productRepository.Value;

        public IBasketRepository BasketRepository => _basketRepository.Value;

        public Task SaveAsync() => _dbContext.SaveChangesAsync();
    }
}
