using System;

namespace Ninefold.API.Storage
{
    public class HeaderAttribute : Attribute
    {
        public string Name { get; private set; }

        public HeaderAttribute (string headerName)
        {
            Name = headerName;
        }
    }
}