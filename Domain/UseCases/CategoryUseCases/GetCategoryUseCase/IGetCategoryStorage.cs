using Domain.Models;

namespace Domain.UseCases.CategoryUseCases.GetCategoryUseCase
{
    public interface IGetCategoryStorage
    {
        Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken); 
    }
}
