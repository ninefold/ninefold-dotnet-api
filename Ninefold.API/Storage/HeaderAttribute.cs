using System;

namespace Ninefold.API.Storage
{
    public class HeaderAttribute : Attribute
    {
        public string Name { get; private set; }

        public bool Serialise { get; private set; }

        public HeaderAttribute (string headerName) : this (headerName, true)
        { }

        public HeaderAttribute(string headerName, bool serialise)
        {
            Name = headerName;
            Serialise = serialise;
        }
    }
}