using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Core;
using Ninefold.API.Storage.Messages;
using RestSharp;

namespace Ninefold.API.Storage.Commands
{
    public class CreateObject
    {
        readonly INinefoldService _storageService;

        public CreateObject(INinefoldService storageService)
        {
            _storageService = storageService;
        }

        public CreateObjectResponse Execute()
        {
            var request = new RestRequest("rest/objects", Method.POST);
            request.AddHeader("x-emc-date", DateTime.UtcNow.ToString());

            return _storageService.ExecuteRequest<CreateObjectResponse>(request);
        }
    }
}
