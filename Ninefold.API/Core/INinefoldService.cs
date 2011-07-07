using System;
using System.Security;
using Ninefold.API.Compute.Messages;
using RestSharp;

namespace Ninefold.API.Core
{
    public interface INinefoldService
    {
        RestClient Client { get;  }

        TReturnType ExecuteRequest<TReturnType>(RestRequest request) where TReturnType : class, new();
    }
}