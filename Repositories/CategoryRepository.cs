using Entity;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using Shared;

namespace Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(GlazySkinDbContext glazySkinDbContext) : base(glazySkinDbContext) { }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
        {
            var categories = await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync(); 
            return categories;
        }

        public void CreateCategory(Category category) => Create(category); 


        public async Task<Category> GetCategoryByIdAsync(Guid id, bool trackChanges)
        {
            var category = await FindByCondition(c => c.CategoryId.Equals(id), trackChanges).SingleOrDefaultAsync();
            return category; 
        }

        public bool CheckByNameCategoryExists(string name, bool trackChanges)
        {
            var category = FindByCondition(c => c.Name.Equals(name), trackChanges);

            if (category is null)
                return true;
            return false;
        }


        public async Task<IEnumerable<Category>> GetCategoryByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)=>
            await FindByCondition(c => ids.Contains(c.CategoryId), trackChanges).ToListAsync();

        public void DeleteCategory(Category category) => Delete(category); 
    }
}
