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

        public ListTemplatesResponse ListTemplates(ListTemplatesRequest request)
        {
            var command = new ListTemplates(_apiKey, _secret, _baseUri,  _authenticator, _builder) { Parameters = request };
            return (ListTemplatesResponse) ((ICommandExecutor)this).Execute(command);
        }

        public ListAccountsResponse ListAccounts(ListAccountsRequest request)
        {
            var command = new ListAccounts(_apiKey, _secret, _baseUri, _authenticator, _builder) { Parameters = request };
            return (ListAccountsResponse) ((ICommandExecutor)this).Execute(command);
        }

        public ListServiceOfferingsResponse ListServiceOfferings(ListServiceOfferingsRequest request)
        {
            var command = new ListServiceOfferings(_apiKey, _secret, _baseUri, _authenticator, _builder) { Parameters = request };
            return (ListServiceOfferingsResponse)((ICommandExecutor)this).Execute(command);
        }

        public ListVirtualMachinesResponse ListVirtualMachines(ListVirtualMachinesRequest request)
        {
            var command = new ListVirtualMachines(_apiKey, _secret, _baseUri, _authenticator, _builder) { Parameters = request };
            return (ListVirtualMachinesResponse)((ICommandExecutor)this).Execute(command);
        }

        public ListTemplatePermissionsResponse ListTemplatePermissions(ListTemplatePermissionsRequest request)
        {
            var command = new ListTemplatePermissions(_apiKey, _secret, _baseUri, _authenticator, _builder) { Parameters = request };
            return (ListTemplatePermissionsResponse)((ICommandExecutor)this).Execute(command);
        }

        public ListIsosResponse ListIsos(ListIsosRequest request)
        {
            var command = new ListIsos(_apiKey, _secret, _baseUri, _authenticator, _builder) { Parameters = request };
            return (ListIsosResponse)((ICommandExecutor)this).Execute(command);
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