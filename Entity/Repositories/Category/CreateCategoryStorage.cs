using Domain.Models;
using Domain.UseCases.CategoryUseCases.CreateCategoryUseCase;

namespace Entity.Repositories.Category
{
    public class CreateCategoryStorage(GlazySkinDbContext _dbContext) : ICreateCategoryStorage
    {
        public async Task Execute(Guid id, string name, IEnumerable<Product>? products, CancellationToken cancellationToken)
        {
            var category = new Models.Category()
            {
                CategoryId = id,
                Name = name, 
                Products = (ICollection<Models.Product>)products
            };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync(); 
        }
    }
}
