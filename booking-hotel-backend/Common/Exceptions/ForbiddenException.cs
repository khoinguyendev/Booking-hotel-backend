using System.Net;

namespace booking_hotel_backend.Common.Exceptions
{
    public class ForbiddenException : ApiException
    {
        public ForbiddenException(string code, string message)
            : base((int)HttpStatusCode.Forbidden, code, message)
        {
        }
    }
}