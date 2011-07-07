using System;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using RestSharp;

namespace Ninefold.API.Compute.Commands
{
    public class StartVirtualMachine
    {
        readonly INinefoldService _computeService;

        public string ApiKey { get; set; }

        public string MachineId { get; set; }

        public StartVirtualMachine()
        {
            _computeService = new ComputeService();
        }

        public StartVirtualMachine(INinefoldService ninefoldService)
        {
            _computeService = ninefoldService;
        }

        public MachineResponse Execute()
        {
            var request = BuildRequest();
            return _computeService.ExecuteRequest<MachineResponse>(request);
        }

        private RestRequest BuildRequest()
        {
            var request = new RestRequest(Method.POST);
            request.AddUrlSegment("command", "startvirtualmachine");
            return request;
        }

        
    }

    public class ComputeService : INinefoldService
    {
        public TReturnType ExecuteRequest<TReturnType>(RestRequest request)
            where TReturnType : class, IResponse
        {
            return default(TReturnType);
        }
    }
}
