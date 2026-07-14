using System.Net;

namespace booking_hotel_backend.Common.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException(string code, string message)
            : base((int)HttpStatusCode.BadRequest, code, message)
        {
        }
    }
}