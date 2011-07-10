using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Tests.Fakes
{
    public class StorageServiceStub : INinefoldService
    {
        public RestRequest Request { get; set; }

        public RestClient Client { get; set; }

        public Uri Uri { get; set; }

        public IList<HttpHeader> Headers { get; private set; }

        public HttpFactoryStub HttpFactory { get; set; }

        public StorageServiceStub()
        {            
            HttpFactory = new HttpFactoryStub();
            Client = new RestClient
                         {
                BaseUrl = "http://tempuri.org/",
                HttpFactory = HttpFactory
            };
        }

        public TReturnType ExecuteRequest<TReturnType>(RestRequest request)
            where TReturnType : class, new()
        {
            Request = request;
            Uri = Client.BuildUri(request);

            Client.Execute(request);
            Headers = HttpFactory.HttpStub.Headers;

            return default(TReturnType);
        }
    }
}
