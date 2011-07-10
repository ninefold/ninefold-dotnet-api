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

        public string GroupACL { get; set; }

        public IEnumerable<KeyValuePair<string, string>> ACL { get; set; }

        public CreateObject(INinefoldService storageService)
        {
            _storageService = storageService;
        }

        public CreateObjectResponse Execute()
        {
            var request = new RestRequest("rest/objects", Method.POST);
            request.AddHeader("x-emc-date", DateTime.UtcNow.ToString());
            request.AddHeader("x-emc-groupacl", string.Format("other={0}", GroupACL));
            request.AddHeader("x-emc-acl", BuildACLString());

            return _storageService.ExecuteRequest<CreateObjectResponse>(request);
        }

        private string BuildACLString()
        {
            if ((ACL == null) || (ACL.Count() == 0))
            {
                return "NONE";
            }

            var aclString = new StringBuilder();

            foreach (var aclEntry in ACL)
            {
                aclString.Append(string.Format("{0}{1}", aclEntry.Key, aclEntry.Value));
            }

            return aclString.ToString();
        }
    }
}
