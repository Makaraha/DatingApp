using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions.ServerExceptions
{
    public class BadRequestException : BaseServerException
    {
        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(HttpStatusCode.BadRequest, message, innerException)
        {
        }
    }
}
