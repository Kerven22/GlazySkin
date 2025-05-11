using Entity.Models;
using Shared;

namespace ServiceContracts
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories(bool trackChanges);

        CategoryDto CreateCategory(CategoryForCreationDto categoryDto);

        CategoryDto GetCategoryById(Guid id, bool trackChanges);

        IEnumerable<CategoryDto> GetCategoriesByIds(IEnumerable<Guid> ids, bool trackChanges);

        (IEnumerable<CategoryDto> categories, string ids) CreateCategoriesCollection(
            IEnumerable<CategoryForCreationDto> categoryForCreationDtosCollection);

        void DeleteCategory(Guid categoryId, bool trackChanges);
    }
    
}
