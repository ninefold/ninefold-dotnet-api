using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Storage
{
    public class StorageService : INinefoldService
    {
        readonly RestClient _client;
        public RestClient Client
        {
            get { return _client; }
        }

        public StorageService(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public TReturnType ExecuteRequest<TReturnType>(RestRequest request) where TReturnType : class, new()
        {
            return _client.Execute<TReturnType>(request).Data;
        }
    }
}
