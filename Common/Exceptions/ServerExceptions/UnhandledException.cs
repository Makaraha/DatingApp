using System.Net;

namespace Common.Exceptions.ServerExceptions
{
    public class UnhandledException : BaseServerException
    {
        public UnhandledException(string message) : base(HttpStatusCode.InternalServerError, message) { }

        public UnhandledException(string message, Exception innerException) 
            : base(HttpStatusCode.InternalServerError, message, innerException) { }
    }
}
