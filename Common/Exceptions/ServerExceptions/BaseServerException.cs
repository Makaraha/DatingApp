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

        public BaseServerException(HttpStatusCode statusCode, string message, Exception innerException) 
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
