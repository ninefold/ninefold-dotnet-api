using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Storage
{
    public class StorageService : INinefoldService
    {
        public RestClient Client { get; private set; }

        public StorageService(string baseUrl)
        {
            Client = new RestClient(baseUrl);
        }

        public TReturnType ExecuteRequest<TReturnType>(RestRequest request) where TReturnType : class, new()
        {
            var response = Client.Execute<TReturnType>(request);
            if (response.ErrorException != null) throw new NinefoldApiException(response.ErrorException);
            return response.Data;
        }
    }
}
