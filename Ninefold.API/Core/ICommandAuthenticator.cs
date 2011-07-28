using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Ninefold.API.Core
{
    public interface  ICommandAuthenticator
    {
        string GenerateRequestSignature(WebRequest request, string base64Secret);
    }
}
