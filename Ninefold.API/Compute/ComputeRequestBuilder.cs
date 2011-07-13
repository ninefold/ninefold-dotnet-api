using System;
using System.Collections.Generic;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute
{
    public class ComputeRequestBuilder : IRequestBuilder
    {
        public IRestRequest GenerateRequest(Dictionary<string, string> parameters)
        {
            //order and push params into url

            return new RestRequest(string.Empty, Method.POST);
        }
    }
}
