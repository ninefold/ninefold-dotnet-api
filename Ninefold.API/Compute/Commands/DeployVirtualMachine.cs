using System;
using System.Net;
using Ninefold.Compute.Messages;
using Ninefold.Core;
using Ninefold.Storage;

namespace Ninefold.Compute.Commands
{
    public class DeployVirtualMachine : ICommand
    {
        readonly IComputeCommandAuthenticator _authenticator;
        readonly IComputeRequestBuilder _builder;
        readonly string _apiKey;
        readonly string _secret;
        readonly string _baseUri;

        public DeployVirtualMachine(string apiKey, string secret, string baseUri, IComputeCommandAuthenticator authenticator, IComputeRequestBuilder builder)
        {
            _authenticator = authenticator;
            _secret = secret;
            _apiKey = apiKey;
            _builder = builder;
            _baseUri = baseUri;
        }
        
        public DeployVirtualMachineRequest Parameters { get; set; }
        
        public HttpWebRequest Prepare()
        {
            return (HttpWebRequest)_builder.GenerateRequest(Parameters, _authenticator, _baseUri, _apiKey, _secret);            
        }

        public ICommandResponse ParseResponse(WebResponse webResponse)
        {
            return new MachineResponse();
        }
    }

    public interface IComputeCommandRequest
    {
        string Command { get; }
    }
}
