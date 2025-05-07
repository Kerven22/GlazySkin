namespace Servicies.Exceptions;

public sealed class CategoryNotFoundException:NotFoundException
{
    public CategoryNotFoundException(Guid id):base(ErrorCode.Gone, $"Category with Id: {id} was not found"){}
}