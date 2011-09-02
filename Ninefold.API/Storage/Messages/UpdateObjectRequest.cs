using System;
using System.ComponentModel.DataAnnotations;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class UpdateObjectRequest : ICommandRequest
    {
        [Required]
        public Uri Resource { get; set; }

        public byte[] Content { get; set; }

        public string RangeSpecification { get; set; }

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

        [Header("x-emc-listable-tags")]
        public string ListableTags { get; set; }

        [Header("x-emc-user-tags")]
        public string UserTags { get; set; }
    }
}