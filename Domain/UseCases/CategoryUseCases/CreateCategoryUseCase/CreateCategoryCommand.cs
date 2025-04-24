using Domain.Models;

namespace Domain.UseCases.CategoryUseCases.CreateCategoryUseCase
{
    public record CreateCategoryCommand(string Name, IEnumerable<Product>? Products); 
}
