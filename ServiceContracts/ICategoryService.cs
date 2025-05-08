using Entity.Models;
using Shared;

namespace ServiceContracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories(bool trackChanges);

        CategoryDto CreateCategory(CategoryForCreationDto categoryDto);

        CategoryDto GetCategoryById(Guid id, bool trackChanges); 
    }
    
}
