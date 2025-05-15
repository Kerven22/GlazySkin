  using Entity.Models;
  using Shared;

  namespace RepositoryContracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);

        void CreateCategory(Category category);

        Task<Category> GetCategoryByIdAsync(Guid id, bool trackChanges);
        bool CheckByNameCategoryExists(string name, bool trackChanges);

        Task<IEnumerable<Category>> GetCategoryByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

        void DeleteCategory(Category category); 
    }
}

