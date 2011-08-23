using System;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class GetObjectAclRequest : ICommandRequest
    {
        public Uri Resource { get; set; }
    }
}