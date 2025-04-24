using Domain.Models;

namespace Domain.UseCases.CategoryUseCases.GetCategoryUseCase
{
    public class GetCategoryUseCase(IGetCategoryStorage _getCategory) : IGetCategoryUseCase
    {
        public async Task<IEnumerable<Category>> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _getCategory.GetCategories(cancellationToken);

            return categories; 
        }
    }
}
