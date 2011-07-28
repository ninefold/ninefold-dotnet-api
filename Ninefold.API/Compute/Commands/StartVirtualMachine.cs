using System;
using System.Net;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute.Commands
{
    public class StartVirtualMachine : ICommand
    {
        readonly ICommandAuthenticator _authenticator;
        readonly IRestClient _client;
        readonly IComputeRequestBuilder _computeRequestService;
        readonly string _apiKey;
        readonly string _base64Secret;

        public StartVirtualMachineRequest Parameters { get; set; }

        public StartVirtualMachine(string apiKey, 
                                                    string base64Secret, 
                                                    string serviceUrlRoot, 
                                                    ICommandAuthenticator authenticator, 
                                                    IComputeRequestBuilder computeRequestService)
        {
            _computeRequestService = computeRequestService;
            _authenticator = authenticator;
            _apiKey = apiKey;
            _base64Secret = base64Secret;
            _client = new RestClient(serviceUrlRoot);
        }

        public ICommandResponse Execute()
        {
            var request = _computeRequestService.GenerateRequest(Parameters, _apiKey);
            _authenticator.AuthenticateRequest(WebRequest.Create(""), _base64Secret);//((RestClient)_client).BuildUri((RestRequest)request), _base64Secret);
            return _client.Execute<MachineResponse>((RestRequest)request).Data;
        }

        public void Prepare()
        {
            
        }
    }
}
