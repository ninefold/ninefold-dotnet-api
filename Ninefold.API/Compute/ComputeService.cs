using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute
{
    public class ComputeService : INinefoldService
    {
        public RestClient Client { get; private set; }

        public ComputeService(string rootUrl)
        {
            Client = new RestClient(rootUrl);
        }
        
        public TReturnType ExecuteRequest<TReturnType>(RestRequest request)
            where TReturnType : class, new()
        {
            var response = Client.Execute<TReturnType>(request);
            if (response.ErrorException != null) throw new NinefoldApiException(response.ErrorException);
            return response.Data;
        }
    }
}