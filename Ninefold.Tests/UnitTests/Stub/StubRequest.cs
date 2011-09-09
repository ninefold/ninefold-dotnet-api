using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.Compute;

namespace Ninefold.API.Tests.UnitTests.Stub
{
    public class StubRequest : IComputeCommandRequest
    {
        public string Command
        {
            get { return "stubRequest"; }
        }

        public string ParameterOne { get; set; }
        public string ParameterTwo { get; set; }
    }
}
