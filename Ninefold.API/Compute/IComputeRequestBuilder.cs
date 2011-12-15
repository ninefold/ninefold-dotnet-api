using System.Net;

namespace Ninefold.Compute
{
    public interface IComputeRequestBuilder
    {
        WebRequest GenerateRequest(IComputeCommandRequest request, 
                                                            IComputeCommandAuthenticator authenticator, 
                                                            string baseUri,
                                                            string apiKey, 
                                                            string secret);
    }
}