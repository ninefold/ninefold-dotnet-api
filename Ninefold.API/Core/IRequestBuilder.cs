using System.Collections.Generic;
using RestSharp;

namespace Ninefold.API.Core
{
    public interface IRequestBuilder
    {
        IRestRequest GenerateRequest(Dictionary<string, string> parameters);
    }
}
