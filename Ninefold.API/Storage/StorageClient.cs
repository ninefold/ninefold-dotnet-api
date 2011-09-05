using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Ninefold.Core;
using Ninefold.Storage.Commands;
using Ninefold.Storage.Messages;

namespace Ninefold.Storage
{
    public class StorageClient : ICommandExecutor
    {
        const string DefaultStorageRootUrl = "http://onlinestorage.ninefold.com/rest/";

        readonly string _userId;
        readonly string _secret;
        readonly string _baseUrl;
        readonly IStorageCommandBuilder _builder;
        readonly IStorageCommandAuthenticator _authenticator;

        public IStorageCommandBuilder Builder { get { return _builder; } }
        public IStorageCommandAuthenticator Authenticator { get { return _authenticator; } }

        public StorageClient(string userId, string base64Secret)
            : this (userId, base64Secret, DefaultStorageRootUrl)
        { }

        public StorageClient(string userId, string base64Secret, string storageServiceRootUrl)
        {
            _userId = userId;
            _secret = base64Secret;
            _baseUrl = storageServiceRootUrl;
            _builder = new StorageCommandBuilder();
            _authenticator = new StorageCommandAuthenticator();
        }

        public ICommandResponse Execute(ICommand command)
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
                            var message = contentDocument.Root.Elements().FirstOrDefault(e => e.Name.LocalName.Equals("message", StringComparison.InvariantCultureIgnoreCase));
                            var code = contentDocument.Root.Elements().FirstOrDefault(e => e.Name.LocalName.Equals("code", StringComparison.InvariantCultureIgnoreCase));

                            exception.ErrorMessage = message == null ? string.Empty : message.Value;
                            exception.Code = code == null ? string.Empty : code.Value;
                        }
                    }
                }

                throw exception;
            }
        }

        public CreateObjectResponse CreateObject(CreateObjectRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new CreateObject(_userId, _secret, _builder, _authenticator) { Parameters = request };

            return (CreateObjectResponse) ((ICommandExecutor) this).Execute(command);
        }

        public DeleteObjectResponse DeleteObject(DeleteObjectRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new DeleteObject(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (DeleteObjectResponse)((ICommandExecutor) this).Execute(command);
        }

        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new GetObject(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (GetObjectResponse)((ICommandExecutor) this).Execute(command);
        }

        public UpdateObjectResponse UpdateObject(UpdateObjectRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new UpdateObject(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (UpdateObjectResponse)((ICommandExecutor) this).Execute(command);
        }

        public ListNamespaceResponse ListNamespace(ListNamespaceRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new ListNamespace(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (ListNamespaceResponse)((ICommandExecutor) this).Execute(command);
        }

        public SetObjectACLResponse SetObjectACL(SetObjectACLRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new SetObjectACL(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (SetObjectACLResponse)((ICommandExecutor) this).Execute(command);
        }

        public DeleteUserMetadataResponse DeleteUserMetadata(DeleteUserMetadataRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new DeleteUserMetadata(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (DeleteUserMetadataResponse)((ICommandExecutor) this).Execute(command);
        }

        public GetObjectAclResponse GetObjectACL(GetObjectAclRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new GetObjectACL(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (GetObjectAclResponse)((ICommandExecutor) this).Execute(command);
        }

        public GetListableTagsResponse GetListableTags(GetListableTagsRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new GetListableTags(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (GetListableTagsResponse)((ICommandExecutor) this).Execute(command);
        }

        public GetSystemMetadataResponse GetSystemMetadata(GetSystemMetadataRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new GetSystemMetadata(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (GetSystemMetadataResponse)((ICommandExecutor) this).Execute(command);
        }

        public ListObjectsResponse ListObjects(ListObjectsRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new ListObjects(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (ListObjectsResponse)((ICommandExecutor) this).Execute(command);
        }

        public GetUserMetadataResponse GetUserMetadata(GetUserMetadataRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new GetUserMetadata(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (GetUserMetadataResponse)((ICommandExecutor) this).Execute(command);
        }

        public SetUserMetadataResponse SetUserMetadata(SetUserMetadataRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new SetUserMetadata(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (SetUserMetadataResponse)((ICommandExecutor) this).Execute(command);
        }

        private void EnsureAbsoluteUri(IStorageCommandRequest request)
        {
            if (request.Resource.IsAbsoluteUri) return;
            var relativeResourcePath = request.Resource;
            request.Resource = new Uri(new Uri(_baseUrl), relativeResourcePath);
        }
    }
}
