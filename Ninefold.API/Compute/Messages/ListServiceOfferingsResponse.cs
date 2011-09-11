using System.Collections.Generic;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class ListServiceOfferingsResponse : ICommandResponse
    {
        public IEnumerable<ServiceOffering> ServiceOfferings { get; set; }
    }
}