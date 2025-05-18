namespace Servicies.Exceptions;

public class CostRangeBadRequestException:BadRequestException
{
    public CostRangeBadRequestException():base(ErrorCode.BadRequest,"Max cost can't be less than min cost")
    {
        
    }
}