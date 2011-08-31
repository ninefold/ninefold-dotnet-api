using Ninefold.API.Core;

namespace Ninefold.API.Compute
{
    public interface IComputeRequestBuilder
    {
        void GenerateRequest(ICommandRequest request, string apiKey);
    }
}