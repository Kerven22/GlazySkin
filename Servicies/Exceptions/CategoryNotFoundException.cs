namespace Servicies.Exceptions;

public class CategoryNotFoundException:Exception
{
    public CategoryNotFoundException(string name):base($"Category with name: {name} was not found"){}
}