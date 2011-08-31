using System;
using Ninefold.API.Core;
using Ninefold.API.Storage.Commands;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage
{
    public class StoredObject : IStoredObject
    {
        readonly string _userId;
        readonly string _secret;
        readonly string _baseUrl;
        readonly IStorageCommandBuilder _builder;        
        readonly ICommandAuthenticator _authenticator;
        readonly ICommandExecutor _commandExecutor;
        
        public IStorageCommandBuilder Builder { get { return _builder; } }
        public ICommandAuthenticator Authenticator { get { return _authenticator; } }
        public ICommandExecutor Executor {get { return _commandExecutor; }}
        
        public StoredObject(string userId, 
                                        string base64Secret, 
                                        string storageServiceRootUrl)
        {
            _authenticator = new StorageCommandAuthenticator();
            _builder = new StorageCommandBuilder();
            _commandExecutor = new StorageCommandExecutor();
            _secret = base64Secret;
            _baseUrl = storageServiceRootUrl;
            _userId = userId;
        }

        public CreateObjectResponse CreateObject(CreateObjectRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new CreateObject(_userId, _secret, _builder, _authenticator) { Parameters = request };
            
            return (CreateObjectResponse) _commandExecutor.Execute(command);
        }

        public DeleteObjectResponse DeleteObject(DeleteObjectRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new DeleteObject(_userId, _secret, _builder, _authenticator) { Parameters = request } ;
            return (DeleteObjectResponse)_commandExecutor.Execute(command);
        }

        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new GetObject(_userId, _secret, _builder, _authenticator) { Parameters = request };                        
            return (GetObjectResponse)_commandExecutor.Execute(command);
        }

        public UpdateObjectResponse UpdateObject(UpdateObjectRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new UpdateObject(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (UpdateObjectResponse)_commandExecutor.Execute(command);
        }

        public ListNamespaceResponse ListNamespace(ListNamespaceRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new ListNamespace(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (ListNamespaceResponse)_commandExecutor.Execute(command);
        }

        public SetObjectACLResponse SetObjectACL(SetObjectACLRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new SetObjectACL(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (SetObjectACLResponse)_commandExecutor.Execute(command);
        }

        public DeleteUserMetadataResponse DeleteUserMetadata(DeleteUserMetadataRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new DeleteUserMetadata(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (DeleteUserMetadataResponse)_commandExecutor.Execute(command);
        }

        public GetObjectAclResponse GetObjectACL(GetObjectAclRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new GetObjectACL(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (GetObjectAclResponse)_commandExecutor.Execute(command);
        }

        public GetListableTagsResponse GetListableTags(GetListableTagsRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new GetListableTags(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (GetListableTagsResponse)_commandExecutor.Execute(command);
        }

        public GetSystemMetadataResponse GetSystemMetadata(GetSystemMetadataRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new GetSystemMetadata(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (GetSystemMetadataResponse)_commandExecutor.Execute(command);
        }

        public ListObjectsResponse ListObjects(ListObjectsRequest request)
        {
            EnsureAbsoluteUri(request);
            var command = new ListObjects(_userId, _secret, _builder, _authenticator) { Parameters = request };
            return (ListObjectsResponse)_commandExecutor.Execute(command);
        }

        private void EnsureAbsoluteUri(ICommandRequest request)
        {
            if (request.Resource.IsAbsoluteUri) return;
            var relativeResourcePath = request.Resource;
            request.Resource = new Uri(new Uri(_baseUrl), relativeResourcePath);
        }
    }
}
