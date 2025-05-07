namespace Servicies.Exceptions;

public abstract class NotFoundException:Exception
{
    public ErrorCode ErrorCode { get; }

    protected NotFoundException(ErrorCode errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
}