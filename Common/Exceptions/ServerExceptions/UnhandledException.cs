using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions.ServerExceptions
{
    public class UnhandledException : BaseServerException
    {
        public UnhandledException(string message) : base(HttpStatusCode.InternalServerError, message) { }

        public UnhandledException(string message, Exception innerException) 
            : base(HttpStatusCode.InternalServerError, message, innerException) { }
    }
}
