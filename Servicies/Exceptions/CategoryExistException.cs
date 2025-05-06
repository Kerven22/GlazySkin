namespace Servicies.Exceptions;

public class CategoryExistException:Exception
{
    public CategoryExistException(Guid id):base($"Category with name:{id} exists in database"){}
}