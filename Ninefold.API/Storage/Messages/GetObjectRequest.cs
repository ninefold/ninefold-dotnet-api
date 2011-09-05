using System;
using System.ComponentModel.DataAnnotations;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class GetObjectRequest : IStorageCommandRequest
    {
        [Required]
        public Uri Resource { get; set; }
        
        public long LowerRange { get; set; }
        
        public long UpperRange { get; set; }
        
        [Header("x-emc-includemeta")]
        public bool IncludeMeta { get; set; }

        [Header("x-emc-limit")]
        public bool Limit { get; set; }
    }
}