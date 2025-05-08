namespace Servicies.Exceptions;

public class CategoryExistException:Exception
{
    public CategoryExistException(string  name):base($"Category with name:{name} exists in database"){}
}