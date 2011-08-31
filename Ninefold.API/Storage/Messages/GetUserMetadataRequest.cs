using System;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class GetUserMetadataRequest : ICommandRequest
    {
        public Uri Resource { get; set; }
    }
}