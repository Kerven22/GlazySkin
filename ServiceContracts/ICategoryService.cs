using Entity.Models;
using Shared;

namespace ServiceContracts
{
    public interface ICategoryService
    {
        
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges);

        Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto categoryDto);

        Task<CategoryDto> GetCategoryByIdAsync(Guid id, bool trackChanges);

        Task<IEnumerable<CategoryDto>> GetCategoriesByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

        Task<(IEnumerable<CategoryDto> categories, string ids)> CreateCategoriesCollectionAsync(
            IEnumerable<CategoryForCreationDto> categoryForCreationDtosCollection);

        Task DeleteCategoryAsync(Guid categoryId, bool trackChanges);

        Task UpdateCategoryAsync(Guid categoryId, CategoryForUpdate categoryForUpdate, bool trackChanges); 
    }
    
}
