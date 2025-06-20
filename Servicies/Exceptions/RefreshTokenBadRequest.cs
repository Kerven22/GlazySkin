namespace Servicies.Exceptions
{
    public class RefreshTokenBadRequest : Exception
    {
        public RefreshTokenBadRequest() : base("Invalid client request. The token tokenDto has some invalid values.")
        {

        }
    }
}
