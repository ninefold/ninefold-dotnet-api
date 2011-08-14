using System;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class DeleteUserMetadataRequest : ICommandRequest
    {
        public Uri Resource { get; set; }

        [Header("x-emc-tags")]
        public string Tags { get; set; }
    }
}