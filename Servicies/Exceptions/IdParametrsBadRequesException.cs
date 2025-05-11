namespace Servicies.Exceptions;

public class IdParametrsBadRequesException:BadRequestException
{
    public IdParametrsBadRequesException():base(ErrorCode.BadRequest, "Parametr ids is null"){}
}