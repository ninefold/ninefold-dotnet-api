using System;
using System.ComponentModel.DataAnnotations;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class ListObjectsRequest : ICommandRequest
    {
        public Uri Resource { get; set; }

        [Header("x-emc-include-meta")]
        public int IncludeMetadata { get; set; }

        [Required]
        [Header("x-emc-tags")]
        public string Tags { get; set; }

        [Header("x-emc-limit")]
        public int MaxReturnCount { get; set; }

        [Header("x-emc-token")]
        public string Token { get; set; }
    }
}