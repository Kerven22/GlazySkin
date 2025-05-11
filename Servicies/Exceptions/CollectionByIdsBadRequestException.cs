namespace Servicies.Exceptions;

public class CollectionByIdsBadRequestException:BadRequestException
{
    public CollectionByIdsBadRequestException():base(ErrorCode.BadRequest, "Collection count mismatch comparing to ids."){}
}