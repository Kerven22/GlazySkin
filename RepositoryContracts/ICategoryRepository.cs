  using Entity.Models;
  using Shared;

  namespace RepositoryContracts
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories(bool trackChanges);

        void CreateCategory(Category category);

        Category GetCategoryById(Guid id, bool trackChanges);
        bool CheckByNameCategoryExists(string name, bool trackChanges);

        IEnumerable<Category> GetCategoryByIds(IEnumerable<Guid> ids, bool trackChanges);

        void DeleteCategory(Category category); 
    }
}

