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
            var command = new CreateObject(_userId, _secret, _builder, _authenticator);

            if (!request.Resource.IsAbsoluteUri)
            {
                var relativeResourcePath = request.Resource;
                request.Resource = new Uri(new Uri(_baseUrl), relativeResourcePath);
            }

            command.Parameters = request;

            return (CreateObjectResponse) _commandExecutor.Execute(command);
        }

        public DeleteObjectResponse DeleteObject(DeleteObjectRequest request)
        {
            var command = new DeleteObject(_userId, _secret, _builder, _authenticator);

            if (!request.Resource.IsAbsoluteUri)
            {
                var relativeResourcePath = request.Resource;
                request.Resource = new Uri(new Uri(_baseUrl), relativeResourcePath);
            }

            command.Parameters = request;

            return (DeleteObjectResponse)_commandExecutor.Execute(command);
        }
    }
}
