using System;
using Ninefold.API.Core;
using Ninefold.API.Storage.Commands;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Storage
{
    public class StoredObject
    {
        readonly string _userId;
        readonly string _secret;
        readonly string _baseUrl;
        readonly IStorageRequestBuilder _builder;        
        readonly ICommandAuthenticator _commandAuthenticator;
        readonly ICommandExecutor _commandExecutor;
        
        public IStorageRequestBuilder Builder { get { return _builder; } }
        public ICommandAuthenticator Authenticator { get { return _commandAuthenticator; } }
        public ICommandExecutor Executor {get { return _commandExecutor; }}
        
        public StoredObject(string userId, 
                                        string base64Secret, 
                                        string storageServiceRootUrl)
        {
            _commandAuthenticator = new StorageRequestAuthenticator();
            _builder = new StorageRequestBuilder();
            _commandExecutor = new StorageCommandExecutor();
            _secret = base64Secret;
            _baseUrl = storageServiceRootUrl;
            _userId = userId;
        }

        public CreateObjectResponse CreateObject(CreateObjectRequest request)
        {
            var command = new CreateObject(_userId, _secret, _builder, _commandAuthenticator, _baseUrl);

            if (!request.Resource.IsAbsoluteUri)
            {
                var relativeResourcePath = request.Resource;
                request.Resource = new Uri(new Uri(_baseUrl), relativeResourcePath);
            }

            command.Parameters = request;

            return (CreateObjectResponse) _commandExecutor.Execute(command);
        }
    }
}
