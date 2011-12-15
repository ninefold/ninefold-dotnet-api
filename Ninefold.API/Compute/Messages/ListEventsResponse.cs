using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Ninefold.Core;

namespace Ninefold.Compute.Messages
{
    public class ListEventsResponse : ICommandResponse
    {
        public IEnumerable<Event> Events { get; set; }
    }

    public class Event
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
        public int DomainId { get; set; }
        public string Level { get; set; }
        public int ParentId { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }

        public static Event From(XElement eventElement)
        {
            return new Event
                       {
                           Account = eventElement.ExtractValue("account"),
                           Created = eventElement.ExtractValue<DateTime>("created"),
                           Description = eventElement.ExtractValue("description"),
                           Domain = eventElement.ExtractValue("domain"),
                           DomainId = eventElement.ExtractValue<int>("domainId"),
                           Id = eventElement.ExtractValue<int>("id"),
                           Level = eventElement.ExtractValue("level"),
                           ParentId = eventElement.ExtractValue<int>("parentId"),
                           State = eventElement.ExtractValue("state"),
                           Type = eventElement.ExtractValue("type"),
                           Username = eventElement.ExtractValue("username")
                       };
        }
    }
}
