using System.Xml.XPath;
using AutoMapper;
using Entity.Models;
using RepositoryContracts;
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

        public IEnumerable<CategoryDto> GetCategoriesByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametrsBadRequesException();
            
            var categoriesEntities = _repositoryManager.CategoryRepository.GetCategoryByIds(ids, trackChanges);

            if (ids.Count() != categoriesEntities.Count())
                throw new CollectionByIdsBadRequestException();
            
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categoriesEntities);
            return categoriesDto;
        }

        public (IEnumerable<CategoryDto> categories, string ids) CreateCategoriesCollection(
            IEnumerable<CategoryForCreationDto> categoryForCreationDtosCallection)
        {
            if (categoryForCreationDtosCallection is null)
                throw new CategoryCollectionBadRequest();

            var categoryEntity = _mapper.Map<IEnumerable<Category>>(categoryForCreationDtosCallection);

            foreach (var category in categoryEntity)
            {
                _repositoryManager.CategoryRepository.CreateCategory(category);
            }
            _repositoryManager.SaveAsync();

            var categoryCollectionToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntity);
            var ids = string.Join(',', categoryCollectionToReturn.Select(c => c.Id));
            return (categories: categoryCollectionToReturn, ids: ids);
        }

        public void DeleteCategory(Guid categoryId, bool trackChanges)
        {
            var category = _repositoryManager.CategoryRepository.GetCategoryById(categoryId, trackChanges);
            if (category is null)
                throw new CategoryNotFoundException(categoryId); 
            _repositoryManager.CategoryRepository.DeleteCategory(category);
            _repositoryManager.SaveAsync();
        }
    }
}
