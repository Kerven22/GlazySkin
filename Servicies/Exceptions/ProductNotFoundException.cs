namespace Servicies.Exceptions;

public class ProductNotFoundException:NotFoundException
{
    public ProductNotFoundException(Guid productId) : base(ErrorCode.Gone, $"Product with Id: {productId} was not found")
    {
    }
}