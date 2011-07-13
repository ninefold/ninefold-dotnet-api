using System;
using System.Collections.Generic;
using System.Net;
using Ninefold.API.Compute.Commands;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Tests.Fakes
{
    public class ComputeServiceStub : ICommandExecutor 
    {
        public RestRequest Request { get; set; }

        public RestClient Client { get; set; }

        public Uri RequestedUri { get; set; }

        public ComputeServiceStub()
        {
            Client = new RestClient("http://tempuri.org/");
        } 

        public ICommandResponse Execute(ICommand command)
        {
            //Request = request;
            //RequestedUri = Client.BuildUri(request);
            return default(ICommandResponse);
        }
    }

    public class StubHttpFactory : IHttpFactory
    {
        public IHttp Create()
        {
            return new StubHttp();
        }
    }

    public class StubHttp : IHttp
    {
        public void DeleteAsync(Action<HttpResponse> action)
        {
            throw new NotImplementedException();
        }

        public void GetAsync(Action<HttpResponse> action)
        {
            throw new NotImplementedException();
        }

        public void HeadAsync(Action<HttpResponse> action)
        {
            throw new NotImplementedException();
        }

        public void OptionsAsync(Action<HttpResponse> action)
        {
            throw new NotImplementedException();
        }

        public void PostAsync(Action<HttpResponse> action)
        {
            throw new NotImplementedException();
        }

        public void PutAsync(Action<HttpResponse> action)
        {
            throw new NotImplementedException();
        }

        public HttpResponse Delete()
        {
            throw new NotImplementedException();
        }

        public HttpResponse Get()
        {
            throw new NotImplementedException();
        }

        public HttpResponse Head()
        {
            throw new NotImplementedException();
        }

        public HttpResponse Options()
        {
            throw new NotImplementedException();
        }

        public HttpResponse Post()
        {
            throw new NotImplementedException();
        }

        public HttpResponse Put()
        {
            throw new NotImplementedException();
        }

        public ICredentials Credentials
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string UserAgent
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int Timeout
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool FollowRedirects
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int? MaxRedirects
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IList<HttpHeader> Headers
        {
            get { throw new NotImplementedException(); }
        }

        public IList<HttpParameter> Parameters
        {
            get { throw new NotImplementedException(); }
        }

        public IList<HttpFile> Files
        {
            get { throw new NotImplementedException(); }
        }

        public IList<HttpCookie> Cookies
        {
            get { throw new NotImplementedException(); }
        }

        public string RequestBody
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string RequestContentType
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Uri Url
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IWebProxy Proxy
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
