using Domain.Models;

namespace Domain.UseCases.CategoryUseCases.CreateCategoryUseCase
{
    public interface ICreateCategoryStorage
    {
        Task Execute(Guid id, string name, IEnumerable<Product>? products, CancellationToken cancellationToken); 
    }
}
