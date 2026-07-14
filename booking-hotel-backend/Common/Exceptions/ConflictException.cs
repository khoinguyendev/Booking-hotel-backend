using System.Net;

namespace booking_hotel_backend.Common.Exceptions
{
    public class ConflictException : ApiException
    {
        public ConflictException(string code, string message)
            : base((int)HttpStatusCode.Conflict, code, message)
        {
        }
    }
}