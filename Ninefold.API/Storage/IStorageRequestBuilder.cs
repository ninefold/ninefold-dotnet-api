using System;
using System.Net;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Storage
{
    public interface IStorageRequestBuilder
    {
        WebRequest GenerateRequest(ICommandRequest request, Uri resource, string userId, Method requestMethod);
    }
}