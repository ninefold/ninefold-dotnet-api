using System;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Tests.Fakes
{
    public class ComputeServiceStub : INinefoldService 
    {
        public RestRequest Request { get; set; }

        public RestClient Client { get; set; }

        public IResponse Response { get; set; }

        public Uri RequestedUri { get; set; }

        public TReturnType ExecuteRequest<TReturnType>(RestRequest request)
            where TReturnType : class, IResponse
        {
            Request = request;
            RequestedUri = Client.BuildUri(request);
            return Response as TReturnType;
        }

        
    }
}
