using Entity.Models;
using Shared;

namespace ServiceContracts
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories(bool trackChanges);

        CategoryDto CreateCategory(string Name); 
    }
}
