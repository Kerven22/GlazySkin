namespace Domain.UseCases.CategoryUseCases.CreateCategoryUseCase
{
    public interface ICreateCategoryUseCase
    {
        Task CreateCategory(CreateCategoryCommand createCategory, CancellationToken cancellationToken); 
    }
}
