using Entity;
using Entity.Models;
using RepositoryContracts;
using Shared;

namespace Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(GlazySkinDbContext glazySkinDbContext) : base(glazySkinDbContext) { }

        public IEnumerable<Category> GetAllCategories(bool trackChanges)
        {
            var categories = FindAll(trackChanges).OrderBy(c => c.Name).ToList(); 
            return categories;
        }

        public void CreateCategory(CategoryDto categoryDto)
        {
            var categoryEntity = new Category()
            {
                CategoryId = categoryDto.Id,
                Name = categoryDto.Name
            };
            Create(categoryEntity);
        }

        public CategoryDto GetCategoryById(Guid id, bool trackChanges)
        {
            var category = FindByCondition(c => c.CategoryId.Equals(id), trackChanges).SingleOrDefault();
            var categoryDto = new CategoryDto(category.CategoryId, category.Name); 
            return categoryDto; 
        }
    }
}
