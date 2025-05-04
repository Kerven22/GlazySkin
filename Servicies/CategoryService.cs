using Entity.Models;
using RepositoryContracts;
using Serilog;
using ServiceContracts;
using Shared;

namespace Servicies
{
    internal sealed class CategoryService(IRepositoryManager _repositoryManager):ICategoryService
    {
        public IEnumerable<Category> GetAllCategories(bool trackChanges)
        {
            try
            {
                var categories = _repositoryManager.CategoryRepository.GetAllCategories(trackChanges);
                return categories;

            }
            catch (Exception e)
            {
                Log.Error("Exception Message {e}",e);
                throw;
            }
        }

        public CategoryDto CreateCategory(string name)
        {
            var categoryDto = new CategoryDto(Guid.NewGuid(), name); 
            _repositoryManager.CategoryRepository.CreateCategory((categoryDto));
            _repositoryManager.SaveAsync(); 
            
            return categoryDto; 
        }
    }
}
