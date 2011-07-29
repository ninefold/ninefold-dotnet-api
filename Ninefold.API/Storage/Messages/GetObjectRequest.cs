using System;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class GetObjectRequest : ICommandRequest
    {
        public Uri Resource { get; set; }
        [Header("lowerrange", false)]
        public long LowerRange { get; set; }
        [Header("upperrange", false)]
        public long UpperRange { get; set; }
    }
}