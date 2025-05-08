using AutoMapper;
using Entity.Models;
using RepositoryContracts;
using Serilog;
using ServiceContracts;
using Servicies.Exceptions;
using Shared;

namespace Servicies
{
    public sealed class CategoryService(IRepositoryManager _repositoryManager, IMapper _mapper):ICategoryService
    {
        public IEnumerable<CategoryDto> GetAllCategories(bool trackChanges)
        {
                var categories = _repositoryManager.CategoryRepository.GetAllCategories(trackChanges);
                var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
                return categoriesDto; 
        }

        public CategoryDto CreateCategory(CategoryForCreationDto category)
        {
            var categoryExists = _repositoryManager.CategoryRepository.CheckByNameCategoryExists(category.name,trackChanges:false);
            if (categoryExists)
                throw new CategoryExistException(category.name);
            
            var categoryEntity = _mapper.Map<Category>(category); 
            
            _repositoryManager.CategoryRepository.CreateCategory(categoryEntity);
            _repositoryManager.SaveAsync();

            var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);
            
            return categoryDto; 
        }

        public CategoryDto GetCategoryById(Guid id, bool trackChanges)
        {
            var category = _repositoryManager.CategoryRepository.GetCategoryById(id, trackChanges);
            if (category is null)
                throw new CategoryNotFoundException(id);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto; 
        }
    }
}
