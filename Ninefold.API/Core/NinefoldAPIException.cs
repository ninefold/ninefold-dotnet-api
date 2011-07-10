using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ninefold.API.Core
{
    public class NinefoldApiException : Exception
    {
        const string ExceptionMessage = "An exception occured during the execution of the request. Please check the inner exception for details.";

        public NinefoldApiException(Exception innerException) : base(ExceptionMessage, innerException)
        {}
    }
}
