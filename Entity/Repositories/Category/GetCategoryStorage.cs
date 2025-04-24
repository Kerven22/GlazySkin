using Domain.UseCases.CategoryUseCases.GetCategoryUseCase;
using Microsoft.EntityFrameworkCore;

namespace Entity.Repositories.Category
{
    public class GetCategoryStorage(GlazySkinDbContext _dbContext) : IGetCategoryStorage
    {
        public async Task<IEnumerable<Domain.Models.Category>> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _dbContext.Categories.Select(c => new Domain.Models.Category()
            {
                Id = c.CategoryId, 
                Name = c.Name, 

            }).ToListAsync(cancellationToken);

            return categories; 
        }
    }
}
