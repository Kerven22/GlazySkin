using Entity;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
namespace Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected GlazySkinDbContext _dbContext;
        public RepositoryBase(GlazySkinDbContext glazySkinDbContext) => _dbContext = glazySkinDbContext; 

        public IQueryable<T> FindAll(bool trackCahanges) =>
            !trackCahanges ? _dbContext.Set<T>().AsNoTracking() :
                _dbContext.Set<T>();

        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? _dbContext.Set<T>().Where(expression).AsNoTracking() :
            _dbContext.Set<T>().Where(expression);

        public async Task Create(T entity) => await _dbContext.Set<T>().AddAsync(entity);

        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

        public void Update(T entity) => _dbContext.Set<T>().Update(entity); 
    }
}
