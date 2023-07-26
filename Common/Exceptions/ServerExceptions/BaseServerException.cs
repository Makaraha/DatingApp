using System.Net;

namespace Common.Exceptions.ServerExceptions
{
    public class BaseServerException : Exception
    {
        public readonly HttpStatusCode StatusCode;

        public BaseServerException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
