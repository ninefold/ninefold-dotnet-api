using Ninefold.Core;

namespace Ninefold.Compute
{
    internal interface IComputeRequestBuilder
    {
        void GenerateRequest(ICommandRequest request, string apiKey);
    }
}