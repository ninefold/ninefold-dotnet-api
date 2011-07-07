using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Tests.Fakes
{
    public class ComputeServiceStub : INinefoldService 
    {
        public RestRequest Request { get; set; }

        public IResponse Response { get; set; }

        public TReturnType ExecuteRequest<TReturnType>(RestRequest request)
            where TReturnType : class, IResponse
        {
            Request = request;
            return Response as TReturnType;
        }


    }
}
