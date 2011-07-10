using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace Ninefold.API.Tests.Fakes
{
    public class HttpFactoryStub : IHttpFactory
    {
        public IHttp HttpStub { get; set; }

        public HttpFactoryStub()
        {
            HttpStub = new Http();    
        }

        public IHttp Create()
        {
            return HttpStub;
        }
    }
}
