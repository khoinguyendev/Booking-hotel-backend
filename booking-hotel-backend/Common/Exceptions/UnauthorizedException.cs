using System.Net;

namespace booking_hotel_backend.Common.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string code, string message)
            : base((int)HttpStatusCode.Unauthorized, code, message)
        {
        }
    }
}