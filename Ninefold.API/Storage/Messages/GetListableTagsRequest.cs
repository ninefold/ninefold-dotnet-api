using System;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class GetListableTagsRequest : IStorageCommandRequest
    {
        public Uri Resource { get; set; }

        [Header("x-emc-token")]
        public string Token { get; set; }

        [Header("x-emc-tags")]
        public string Tags { get; set; }
    }
}