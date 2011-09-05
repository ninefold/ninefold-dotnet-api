using System;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class SetUserMetadataRequest : IStorageCommandRequest
    {
        public Uri Resource { get; set; }

        [Header("x-emc-meta")]
        public string Tags { get; set; }

        [Header("x-emc-listable-meta")]
        public string ListableTags { get; set; }
    }
}