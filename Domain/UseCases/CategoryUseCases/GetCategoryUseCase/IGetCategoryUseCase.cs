using Domain.Models;

namespace Domain.UseCases.CategoryUseCases.GetCategoryUseCase
{
    public interface IGetCategoryUseCase
    {
        Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken); 
    }
}
