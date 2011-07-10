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

        public byte[] Content { get; set; }

        public string ContentType { get; set; }

        public string UserId { get; set; }

        public string GroupACL { get; set; }

        public IEnumerable<KeyValuePair<string, string>> ACL { get; set; }

        public IEnumerable<KeyValuePair<string, string>> ListableMetadata { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Metadata { get; set; }

        public IEnumerable<KeyValuePair<string, string>> OptionalHeaders { get; set; }
        
        public CreateObject(INinefoldService storageService)
        {
            _storageService = storageService;
        }

        public CreateObjectResponse Execute()
        {
            var request = new RestRequest("rest/objects", Method.POST);
            request.AddHeader("content-type", ContentType);
            request.AddHeader("content-length", Content.Length.ToString());
            request.AddHeader("x-emc-date", DateTime.UtcNow.ToString());
            request.AddHeader("x-emc-groupacl", string.Format("other={0}", GroupACL));
            request.AddHeader("x-emc-useracl", BuildKeyPairString(ACL));
            request.AddHeader("x-emc-listable-meta", BuildKeyPairString(ListableMetadata));
            request.AddHeader("x-emc-meta", BuildKeyPairString(Metadata));
            request.AddHeader("x-emc-uid", UserId);
            
            foreach (var optionalHeader in OptionalHeaders)
            {
                request.AddHeader(optionalHeader.Key, optionalHeader.Value);
            }

            request.AddBody(Content);

            return _storageService.ExecuteRequest<CreateObjectResponse>(request);
        }

        

        private static string BuildKeyPairString(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            if ((keyValuePairs == null) || (keyValuePairs.Count() == 0)) return string.Empty;

            var keyValueString = new StringBuilder();

            foreach (var pair in keyValuePairs)
            {
                keyValueString.Append(string.Format("{0}={1},", pair.Key, pair.Value));
            }
            keyValueString.Remove(keyValueString.Length - 1, 1);
            
            return keyValueString.ToString();
        }
    }
}
