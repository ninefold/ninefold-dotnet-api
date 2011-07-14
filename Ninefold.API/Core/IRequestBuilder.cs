using System.Collections.Generic;
using RestSharp;

namespace Ninefold.API.Core
{
    public interface IRequestBuilder
    {
        IRestRequest GenerateRequest(ICommandRequest request, string apiKey);
    }
}
