using Ninefold.Core;

namespace Ninefold.Compute
{
    public class VirtualMachine : IVirtualMachine
    {
        readonly string _apiKey;
        readonly string _base64Secret;
        readonly string _serviceUrlRoot;
        readonly IComputeRequestBuilder _computeRequestBuilder;
        readonly ICommandAuthenticator _requestSigner;

        public IComputeRequestBuilder ComputeRequestBuilder { get { return _computeRequestBuilder; } }
        public ICommandAuthenticator Authenticator { get { return _requestSigner; } }
        
        
        public VirtualMachine(string apiKey, string base64Secret, string serviceUrlRoot)
        {
            _apiKey = apiKey;
            _serviceUrlRoot = serviceUrlRoot;
            _base64Secret = base64Secret;
            _computeRequestBuilder = new ComputeRequestBuilder();
            _requestSigner = new ComputeRequestAuthenticator();
        }

    
    }
}
