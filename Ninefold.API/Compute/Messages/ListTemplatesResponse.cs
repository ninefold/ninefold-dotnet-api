using System.Collections.Generic;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class ListTemplatesResponse : ICommandResponse
    {

        public IEnumerable<Template> Templates { get; set; }

    }
}