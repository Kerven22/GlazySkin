using AutoMapper;
using Entity.Models;
using RepositoryContracts;
using ServiceContracts;
using Servicies.Exceptions;
using Shared;

namespace Servicies
{
    public sealed class CategoryService(IRepositoryManager _repositoryManager, IMapper _mapper) : ICategoryService
    {
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges)
        {
            var categories = await _repositoryManager.CategoryRepository.GetAllCategoriesAsync(trackChanges);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoriesDto;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto category)
        {
            var categoryExists = _repositoryManager.CategoryRepository.CheckByNameCategoryExists(category.Name, trackChanges: false);
            if (categoryExists)
                throw new CategoryExistException(category.Name);

            var categoryEntity = _mapper.Map<Category>(category);

            _repositoryManager.CategoryRepository.CreateCategory(categoryEntity);
            await _repositoryManager.SaveAsync();

            var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);

            return categoryDto;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid id, bool trackChanges)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(id, trackChanges);
            if (category is null)
                throw new CategoryNotFoundException(id);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametrsBadRequesException();

            var categoriesEntities = await _repositoryManager.CategoryRepository.GetCategoryByIdsAsync(ids, trackChanges);

            if (ids.Count() != categoriesEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categoriesEntities);
            return categoriesDto;
        }

        public async Task<(IEnumerable<CategoryDto> categories, string ids)> CreateCategoriesCollectionAsync(
            IEnumerable<CategoryForCreationDto> categoryForCreationDtosCallection)
        {
            if (categoryForCreationDtosCallection is null)
                throw new CategoryCollectionBadRequest();

            var categoryEntity = _mapper.Map<IEnumerable<Category>>(categoryForCreationDtosCallection);

            foreach (var category in categoryEntity)
            {
                _repositoryManager.CategoryRepository.CreateCategory(category);
            }
            await _repositoryManager.SaveAsync();

            var categoryCollectionToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntity);
            var ids = string.Join(',', categoryCollectionToReturn.Select(c => c.Id));
            return (categories: categoryCollectionToReturn, ids: ids);
        }

        public async Task DeleteCategoryAsync(Guid categoryId, bool trackChanges)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategoryByIdAsync(categoryId, trackChanges);
            if (category is null)
                throw new CategoryNotFoundException(categoryId);
            _repositoryManager.CategoryRepository.DeleteCategory(category);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateCategoryAsync(Guid categoryId, CategoryForUpdate categoryForUpdate, bool trackChanges)
        {
            var category = _repositoryManager.CategoryRepository.GetCategoryByIdAsync(categoryId, trackChanges);
            if (category is null)
                throw new CategoryNotFoundException(categoryId);
            _mapper.Map(categoryForUpdate, category);
            await _repositoryManager.SaveAsync();
        }
    }
}
