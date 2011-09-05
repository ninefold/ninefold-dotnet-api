using System.Net;

namespace Ninefold.Storage
{
    public interface  IStorageCommandAuthenticator
    {
        void AuthenticateRequest(WebRequest request, string base64Secret);
    }
}
