using System;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class GetSystemMetadataRequest : ICommandRequest
    {
        public Uri Resource { get; set; }

        [Header("x-emc-tags")]
        public string Tags { get; set; }
    }
}
