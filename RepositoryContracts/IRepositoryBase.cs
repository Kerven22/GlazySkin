using System.Linq.Expressions;

namespace RepositoryContracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackCahanges); 
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity); 
    }
}
