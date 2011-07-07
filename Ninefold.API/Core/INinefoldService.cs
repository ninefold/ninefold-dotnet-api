using Ninefold.API.Compute.Messages;
using RestSharp;

namespace Ninefold.API.Core
{
    public interface INinefoldService 
    {
        TReturnType ExecuteRequest<TReturnType>(RestRequest request) where TReturnType : class, IResponse;
    }
}