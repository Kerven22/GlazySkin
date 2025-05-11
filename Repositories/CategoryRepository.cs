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

        public void CreateCategory(Category category) => Create(category); 


        public Category GetCategoryById(Guid id, bool trackChanges)
        {
            var category = FindByCondition(c => c.CategoryId.Equals(id), trackChanges).SingleOrDefault();
            return category; 
        }

        public bool CheckByNameCategoryExists(string name, bool trackChanges)
        {
            var category = FindByCondition(c => c.Name.Equals(name), trackChanges);

            if (category is null)
                return true;
            return false;
        }


        public IEnumerable<Category> GetCategoryByIds(IEnumerable<Guid> ids, bool trackChanges)=>
            FindByCondition(c => ids.Contains(c.CategoryId), trackChanges).ToList();

        public void DeleteCategory(Category category) => Delete(category); 
    }
}
