using System;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class GetUserMetadataRequest : ICommandRequest
    {
        public Uri Resource { get; set; }
    }
}