using System.Net;

namespace booking_hotel_backend.Common.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string code, string message)
            : base((int)HttpStatusCode.NotFound, code, message)
        {
        }
    }
}