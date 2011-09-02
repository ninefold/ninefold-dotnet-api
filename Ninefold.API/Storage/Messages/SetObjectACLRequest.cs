using System;
using System.ComponentModel.DataAnnotations;
using Ninefold.Core;

namespace Ninefold.Storage.Messages
{
    public class SetObjectACLRequest : ICommandRequest
    {        
        [Required]
        public Uri Resource { get; set; }

        [Header("x-emc-useracl")]
        public string UserACL { get; set; }

        [Header("x-emc-groupacl")]
        public string GroupACL { get; set; }       
    }
}