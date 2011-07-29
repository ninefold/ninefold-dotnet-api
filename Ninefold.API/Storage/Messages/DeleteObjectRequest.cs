using System;
using System.ComponentModel.DataAnnotations;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class DeleteObjectRequest : ICommandRequest
    {
        [Required]
        public Uri Resource { get; set; }
    }
}