using System;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class GetUserMetadataRequest : IStorageCommandRequest
    {
        public Uri Resource { get; set; }
    }
}