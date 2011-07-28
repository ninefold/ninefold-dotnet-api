using System;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class DeleteObjectRequest : ICommandRequest
    {
        public Uri Resource { get; set; }
    }
}