using Entity.Models;
using Shared;

namespace ServiceContracts
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories(bool trackChanges);

        CategoryDto CreateCategory(Guid id, string name);

        CategoryDto GetCategoryById(Guid id, bool trackChanges); 
    }
}
