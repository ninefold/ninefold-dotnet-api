using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Tests.Fakes
{
    public class StorageServiceStub : INinefoldService
    {
        public RestRequest Request { get; set; }

        public RestClient Client { get; set; }
        
        public TReturnType ExecuteRequest<TReturnType>(RestRequest request) 
            where TReturnType : class, new()
        {
            Request = request;
            return default(TReturnType);
        }
    }
}
