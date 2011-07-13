using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Ninefold.API.Compute.Commands;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Tests.Fakes
{
    public class StorageServiceStub : Core.ICommandExecutor
    {
        public RestRequest Request { get; set; }

        public RestClient Client { get; set; }

        public Uri Uri { get; set; }

        public StorageServiceStub()
        {            
            Client = new RestClient
                         {
                BaseUrl = "http://tempuri.org/",
            };
        }

        public TReturnType ExecuteRequest<TReturnType>(RestRequest request)
            where TReturnType : class, new()
        {
            Request = request;
            Uri = Client.BuildUri(request);
            
            return default(TReturnType);
        }

        public ICommandResponse Execute(ICommand command)
        {
            throw new NotImplementedException();
        }
    }
}
