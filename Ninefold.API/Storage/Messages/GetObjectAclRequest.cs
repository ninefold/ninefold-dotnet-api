using System;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class GetObjectAclRequest : ICommandRequest
    {
        public Uri Resource { get; set; }
    }
}