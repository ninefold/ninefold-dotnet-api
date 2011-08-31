using System;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class SetUserMetadataRequest : ICommandRequest
    {
        public Uri Resource { get; set; }

        [Header("x-emc-meta")]
        public string Tags { get; set; }

        [Header("x-emc-listable-meta")]
        public string ListableTags { get; set; }
    }
}