using Entity.Models;
using RepositoryContracts;
using Serilog;
using ServiceContracts;
using Servicies.Exceptions;
using Shared;

namespace Servicies
{
    public sealed class CategoryService(IRepositoryManager _repositoryManager):ICategoryService
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

        public CategoryDto CreateCategory(CategoryDto categoryDto)
        {
            var categoryExists = _repositoryManager.CategoryRepository.GetCategoryById(categoryDto.Id, trackChanges: false);
            if (!(categoryExists is null))
                throw new CategoryExistException(categoryDto.Id); 
            
            _repositoryManager.CategoryRepository.CreateCategory((categoryDto));
            _repositoryManager.SaveAsync(); 
            return categoryDto; 
        }

        public CategoryDto GetCategoryById(Guid id, bool trackChanges)
        {
            var categoryDto = _repositoryManager.CategoryRepository.GetCategoryById(id, trackChanges);
            if(categoryDto is null)
                return null;
            return categoryDto; 
        }
    }
}
