using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class CreateObjectRequest : ICommandRequest
    {
        [Required]
        public Uri Resource { get; set; }

        [Required]
        public byte[] Content { get; set; }

        [Header("content-type", false)]
        public string ContentType { get; set; }

        [Header("x-emc-groupacl")]
        public string GroupACL { get; set; }

        [Header("x-emc-useracl")]
        public string ACL { get; set; }

        [Header("x-emc-listable-meta")]
        public string ListableMetadata { get; set; }

        [Header("x-emc-meta")]
        public string Metadata { get; set; }
    }
}
