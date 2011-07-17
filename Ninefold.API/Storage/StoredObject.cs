using Ninefold.API.Compute;
using Ninefold.API.Core;
using Ninefold.API.Storage.Commands;
using Ninefold.API.Storage.Messages;
using RestSharp;

namespace Ninefold.API.Storage
{
    public class StoredObject
    {
        readonly string _userId;
        readonly string _secret
            ;
        readonly IStorageRequestBuilder _storageRequestBuilder;
        readonly IRequestSigningService _requestSigner;
        readonly IRestClient _client;

        public IStorageRequestBuilder StorageRequestBuilder { get { return _storageRequestBuilder; } }
        public IRequestSigningService SigningService { get { return _requestSigner; } }
        public IRestClient RestClient { get { return _client; } }

        public StoredObject(string userId, 
                                        string base64Secret, 
                                        string storageServiceRootUrl)
        {
            _userId = userId;
            _requestSigner = new RequestSigningService();
            _storageRequestBuilder = new StorageRequestBuilder();
            _secret = base64Secret;
            _client = new RestClient(storageServiceRootUrl);
        }

        public CreateObjectResponse CreateObject(CreateObjectRequest request)
        {
            var command = new CreateObject(_userId, _secret, _storageRequestBuilder, _requestSigner, _client);
            return (CreateObjectResponse) command.Execute();
        }
    }
}
