using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Storage
{
    public interface IStorageRequestBuilder
    {
        IRestRequest GenerateRequest(ICommandRequest request, string resource, string userId, Method requestMethod);
    }
}