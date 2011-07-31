using System;
using System.ComponentModel.DataAnnotations;
using Ninefold.API.Core;

namespace Ninefold.API.Storage.Messages
{
    public class ListNamespaceRequest : ICommandRequest
    {
        [Required]
        public Uri Resource { get; set; }
    }
}
