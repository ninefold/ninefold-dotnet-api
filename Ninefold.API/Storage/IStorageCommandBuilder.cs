using System;
using System.Net;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Storage
{
    public interface IStorageCommandBuilder
    {
        HttpWebRequest GenerateRequest(ICommandRequest request, string userId, HttpMethod requestMethod);
    }
}