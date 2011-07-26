using System.Collections.Generic;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class CreateObjectRequest : ICommandRequest
    {
        public string ResourcePath { get; set; }

        public string Base64Content { get; set; }

        [Header("content-type", false)]
        public string ContentType { get; set; }

        [Header("content-length", false)]
        public long ContentLength { get; set; }

        [Header("x-emc-groupacl")]
        public string GroupACL { get; set; }

        [Header("x-emc-useracl")]
        public string ACL { get; set; }

        [Header("x-emc-listable-meta")]
        public string ListableMetadata { get; set; }

        [Header("x-emc-meta")]
        public string Metadata { get; set; }
        
        public IEnumerable<KeyValuePair<string, string>> OptionalHeaders { get; set; }
    }
}
