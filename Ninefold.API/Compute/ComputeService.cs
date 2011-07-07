using System;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute
{
    public class ComputeService : INinefoldService
    {
        public ComputeService(string rootUrl)
        {
            Client = new RestClient(rootUrl);
        }

        public RestClient Client { get; private set; }

        public TReturnType ExecuteRequest<TReturnType>(RestRequest request)
            where TReturnType : class, new()
        {
            return Client.Execute<TReturnType>(request).Data;
        }
    }
}