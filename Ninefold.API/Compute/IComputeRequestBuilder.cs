using Ninefold.Core;

namespace Ninefold.Compute
{
    public interface IComputeRequestBuilder
    {
        void GenerateRequest(ICommandRequest request, string apiKey);
    }
}