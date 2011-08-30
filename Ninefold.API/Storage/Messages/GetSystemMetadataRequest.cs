using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class GetSystemMetadataRequest : ICommandRequest
    {
        public Uri Resource { get; set; }

        [Header("x-emc-tags")]
        public string Tags { get; set; }
    }
}
