using System;
using System.Collections.Generic;
using System.IO;
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
        readonly byte[] _secret;

        public string ResourcePath { get; set; }

        public byte[] Content { get; set; }

        public string ContentType { get; set; }

        public string UserId { get; set; }

        public string GroupACL { get; set; }

        public IEnumerable<KeyValuePair<string, string>> ACL { get; set; }

        public IEnumerable<KeyValuePair<string, string>> ListableMetadata { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Metadata { get; set; }

        public IEnumerable<KeyValuePair<string, string>> OptionalHeaders { get; set; }

        public CreateObject(INinefoldService storageService, byte[] secret)
        {
            _storageService = storageService;
            _secret = secret;
        }

        public CreateObjectResponse Execute()
        {
            if (((Content == null) || (Content.Length == 0)
                && (!ResourcePath[ResourcePath.Length].Equals('/'))))
            {
                throw new ArgumentOutOfRangeException("If resource path is specified as an object content length must be non-zero");
            }

            var request = new RestRequest(Path.Combine("rest/", ResourcePath), Method.POST);
            request.AddHeader("content-type", string.IsNullOrWhiteSpace(ContentType) ? "application/octet-stream" : ContentType);
            request.AddHeader("content-length", Content.Length.ToString());
            request.AddHeader("x-emc-date", DateTime.UtcNow.ToString());
            request.AddHeader("x-emc-groupacl", string.Format("other={0}", GroupACL));
            request.AddHeader("x-emc-useracl", BuildKeyPairString(ACL));
            request.AddHeader("x-emc-listable-meta", BuildKeyPairString(ListableMetadata));
            request.AddHeader("x-emc-meta", BuildKeyPairString(Metadata));
            request.AddHeader("x-emc-uid", UserId);

            if (OptionalHeaders != null)
            {
                foreach (var optionalHeader in OptionalHeaders)
                {
                    request.AddHeader(optionalHeader.Key, optionalHeader.Value);
                }
            }
            
            request.AddBody(Convert.ToBase64String(Content));
            SignRequest(request);

            return _storageService.ExecuteRequest<CreateObjectResponse>(request);
        }

        private void SignRequest(RestRequest request)
        {
            var uri = _storageService.Client.BuildUri(request);
            var hashingAlg = new System.Security.Cryptography.HMACSHA1(_secret);
            var signature = hashingAlg.ComputeHash(Encoding.UTF8.GetBytes(uri.ToString()));
            request.AddHeader("x-emc-signature", Encoding.UTF8.GetString(signature));
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
