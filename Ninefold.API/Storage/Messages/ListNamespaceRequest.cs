using System;
using System.ComponentModel.DataAnnotations;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class ListNamespaceRequest : ICommandRequest
    {
        [Required]
        public Uri Resource { get; set; }
    }
}
