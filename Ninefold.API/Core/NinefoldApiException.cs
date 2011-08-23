using System;
using System.Net;

namespace Ninefold.API.Core
{
    public class NinefoldApiException : WebException
    {
        public NinefoldApiException(Exception webException) :
            base("An exception has occured while processing a command.", webException)
        {}

        public string ErrorMessage { get; set; }

        public string Code { get; set; }
    }
}
