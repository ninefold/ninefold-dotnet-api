using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute
{
    public interface IComputeRequestBuilder
    {
        IRestRequest GenerateRequest(ICommandRequest request, string apiKey);
    }
}