using AutoMapper;
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

        public CategoryDto CreateCategory(CategoryDto categoryDto)
        {
            var categoryExists = _repositoryManager.CategoryRepository.GetCategoryById(categoryDto.Id, trackChanges: false);
            if (categoryExists != null)
                throw new CategoryExistException(categoryDto.Id); 
            
            _repositoryManager.CategoryRepository.CreateCategory((categoryDto));
            _repositoryManager.SaveAsync(); 
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
