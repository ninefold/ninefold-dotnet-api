using System.Net;

namespace Ninefold.API.Core
{
    public interface  ICommandAuthenticator
    {
        void AuthenticateRequest(WebRequest request, string base64Secret);
    }
}
