using System;
using System.ComponentModel.DataAnnotations;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class DeleteObjectRequest : IStorageCommandRequest
    {
        [Required]
        public Uri Resource { get; set; }
    }
}