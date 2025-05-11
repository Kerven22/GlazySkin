namespace Servicies.Exceptions;

public class BadRequestException:Exception
{
    public ErrorCode ErrorCode;

    public BadRequestException(ErrorCode errorCode, string message) : base(message)
    {
        ErrorCode = errorCode; 
    }
}