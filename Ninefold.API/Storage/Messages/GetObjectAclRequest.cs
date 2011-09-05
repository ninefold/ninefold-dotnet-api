using System;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class GetObjectAclRequest : IStorageCommandRequest
    {
        public Uri Resource { get; set; }
    }
}