using System;
using System.Net;

namespace Ninefold.API.Core
{
    public interface IRequestSigningService
    {
        string GenerateRequestSignature(WebRequest request, string base64Secret);
    }
}