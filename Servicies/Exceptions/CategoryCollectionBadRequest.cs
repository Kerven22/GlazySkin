namespace Servicies.Exceptions;

public class CategoryCollectionBadRequest:BadRequestException
{
    public CategoryCollectionBadRequest() : base(ErrorCode.BadRequest,
        "Category collection sent from a client is null."){}
}