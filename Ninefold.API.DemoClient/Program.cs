using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninefold.API.Compute;

namespace Ninefold.API.DemoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var secret = new byte[] {0x0, 0x1};
            var vm = VirtualMachine.Start("ef2422ab22c11", "1", "http://tempuri.org", secret);
        }
    }
}
