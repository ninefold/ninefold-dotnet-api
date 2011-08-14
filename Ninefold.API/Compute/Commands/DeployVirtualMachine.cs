using System;
using System.Collections.Generic;
using System.Net;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute.Commands
{
    public class DeployVirtualMachine : ICommand
    {
        readonly ICommandAuthenticator _authenticator;
        readonly IRestClient _client;
        readonly IComputeRequestBuilder _computeRequestService;
        readonly string _apiKey;
        readonly string _base64Secret;

        public DeployVirtualMachineRequest Parameters { get; set; }

        public DeployVirtualMachine(string apiKey, 
                                                        string base64Secret,
                                                        ICommandAuthenticator authenticator, 
                                                        IComputeRequestBuilder computeRequestService, 
                                                        IRestClient client)
        {
            _authenticator = authenticator;
            _client = client;
            _computeRequestService = computeRequestService;
            _apiKey = apiKey;
            _base64Secret = base64Secret;
        }

        public ICommandResponse ParseResponse(WebResponse response)
        {
            ValidateRequest();

            var request = _computeRequestService.GenerateRequest(Parameters, _apiKey);
            var uri = Uri.UnescapeDataString(((RestClient) _client).BuildUri((RestRequest) request).ToString());
            _authenticator.AuthenticateRequest(WebRequest.Create(""), _base64Secret);//new Uri(uri), _base64Secret);

            //if (response.ErrorException != null) throw new NinefoldApiException(response.ErrorException);
            //var responseMessage = response.Data ?? new MachineResponse();
            //responseMessage.ErrorMessage = response.ErrorMessage;

            //return responseMessage;

            throw new NotImplementedException("Not yet implemented");
        }

        public HttpWebRequest Prepare()
        {
            throw new NotImplementedException();
        }

        private void ValidateRequest()
        {
            if (!string.IsNullOrWhiteSpace(Parameters.Account) && string.IsNullOrWhiteSpace(Parameters.DomainId)) { throw new ArgumentNullException("DomainId", "DomainId must be provided when an Account is provided");}
            if (!string.IsNullOrWhiteSpace(Parameters.DiskOfferingId) && (string.IsNullOrWhiteSpace(Parameters.Size))) { throw new ArgumentOutOfRangeException("Either the DiskOfferingId or the Size parameter can be provided");}            
        }
    }

    public class NinefoldApiException : Exception
    {
        public NinefoldApiException(Exception errorException)
        {
            throw new NotImplementedException();
        }
    }
}
