using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Ninefold.API.Compute;
using Ninefold.API.Storage;

namespace Ninefold.API.DemoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var secret = new byte[] {0x0, 0x1};
            var vm = VirtualMachine.Start("ef2422ab22c11", "1", "http://tempuri.org", secret);

            var storedObject = StorageObject.Create("http://tempuri.org",
                                                    new byte[] { 0x0, 0x1 },
                                                    new byte[] { 0x0, 0x1 },
                                                    new[] {new KeyValuePair<string, string>("steve", "FULL_CONTROL")},
                                                    new[] {new KeyValuePair<string, string>("", "")},
                                                    new[] {new KeyValuePair<string, string>("", "")});

            Console.WriteLine("Object stored to path {0}", storedObject.Location);
        }
    }
}
