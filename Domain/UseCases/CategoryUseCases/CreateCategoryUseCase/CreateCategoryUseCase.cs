
namespace Domain.UseCases.CategoryUseCases.CreateCategoryUseCase
{
    public class CreateCategoryUseCase(ICreateCategoryStorage _categoryStorage) : ICreateCategoryUseCase
    {
        public async Task CreateCategory(CreateCategoryCommand category, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid(); 

            await _categoryStorage.Execute(id,category.Name, category.Products, cancellationToken); 
        }
    }
}
