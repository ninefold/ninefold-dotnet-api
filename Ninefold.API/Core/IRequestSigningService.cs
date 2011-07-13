using System;

namespace Ninefold.API.Core
{
    public interface IRequestSigningService
    {
        string GenerateRequestSignature(Uri uri, string base64Secret);
    }
}