using System.Net;

namespace Ninefold.Core
{
    public interface  ICommandAuthenticator
    {
        void AuthenticateRequest(WebRequest request, string base64Secret);
    }
}
