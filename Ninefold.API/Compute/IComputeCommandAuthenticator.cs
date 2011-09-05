using System;
using System.Net;

namespace Ninefold.Compute
{
    public interface  IComputeCommandAuthenticator
    {
        string AuthenticateRequest(string queryString, string secret);
    }
}
