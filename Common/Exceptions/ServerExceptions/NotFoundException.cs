using System.Net;

namespace Common.Exceptions.ServerExceptions
{
    public class NotFoundException : BaseServerException
    {
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message) { }
    }
}
