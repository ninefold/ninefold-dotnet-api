using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Ninefold.Compute.Commands;
using Ninefold.Compute.Messages;
using Ninefold.Core;

namespace Ninefold.Compute
{
    public class ComputeClient : IComputeClient, ICommandExecutor
    {
        readonly IComputeRequestBuilder _builder;
        readonly IComputeCommandAuthenticator _authenticator;
        readonly string _apiKey;
        readonly string _secret;
        readonly string _baseUri;

        public ComputeClient(string apiKey, string base64Secret)
            : this(apiKey, base64Secret, "https://api.ninefold.com/compute/v1.0/")
        { }

        public ComputeClient(string apiKey, string base64Secret, string computeServiceUriRoot)
        {
            _builder = new ComputeCommandBuilder();
            _authenticator = new ComputeCommandAuthenticator();
            _apiKey = apiKey;
            _secret = base64Secret;
            _baseUri = computeServiceUriRoot;
        }

        public MachineResponse DeployVirtualMachine(DeployVirtualMachineRequest request)
        {
            var command = new DeployVirtualMachine(_apiKey, _secret, _baseUri,  _authenticator, _builder) {Parameters = request};
            return (MachineResponse) ((ICommandExecutor)this).Execute(command);
        }
        
        ICommandResponse ICommandExecutor.Execute(ICommand command)
        {
            var request = command.Prepare();

            try
            {
                var webResponse = (HttpWebResponse)request.GetResponse();
                return command.ParseResponse(webResponse);
            }
            catch (WebException ex)
            {
                var exception = new NinefoldApiException(ex);

                if (ex.Response.ContentLength > 0)
                {
                    var responseStream = ex.Response.GetResponseStream();
                    if ((responseStream != null) && (responseStream.CanRead))
                    {
                        var contentDocument = XDocument.Load(responseStream);
                        if (contentDocument.Root != null)
                        {
                            var message =
                                contentDocument.Root.Elements().FirstOrDefault(
                                    e => e.Name.LocalName.Equals("errortext", StringComparison.InvariantCultureIgnoreCase));
                            var code =
                                contentDocument.Root.Elements().FirstOrDefault(
                                    e => e.Name.LocalName.Equals("errorcode", StringComparison.InvariantCultureIgnoreCase));

                            exception.ErrorMessage = message == null ? string.Empty : message.Value;
                            exception.Code = code == null ? string.Empty : code.Value;
                        }
                    }


                }
                throw exception;
            }
        }
    }
}