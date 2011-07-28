using System;
using System.IO;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.DemoClient
{
    class Program
    {
        static void Main()
        {
            //var computeClient = new ComputeClient("5cd104e23fc947668a6c74fe63fd77e7/godbold_1310683369246", "FGJeXUzxCz5poHoSzRzmMTceuek=");
            //var machineResponse = computeClient.VirtualMachine.Deploy(new DeployVirtualMachineRequest
            //                                                              {
            //                                                                  DisplayName = "Steves Machine",
            //                                                                  Name = "Steve Test",
            //                                                                  TemplateId = "Dummy",
            //                                                                  ZoneId = "Dummy",
            //                                                                  ServiceOfferingId = "Dummy"
            //                                                              });
            //computeClient.VirtualMachine.Start(new StartVirtualMachineRequest { MachineId = machineResponse.Id });

            var demoContent = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "DemoFile.txt"));
            var storageClient = new StorageClient("5cd104e23fc947668a6c74fe63fd77e7/godbold_1310683369246", "FGJeXUzxCz5poHoSzRzmMTceuek=");
            var storedObjectResponse = storageClient.StoredObject.CreateObject(new CreateObjectRequest
                                                                                   {
                                                                                       Content = demoContent,
                                                                                       Resource = new Uri("/rest/objects", UriKind.Relative),
                                                                                       GroupACL = "other=NONE",
                                                                                       ACL = "godbold=FULL_CONTROL",
                                                                                       Metadata = "part1=buy",
                                                                                       ListableMetadata = "part4/part7/part8=quick"
                                                                                   });

            Console.WriteLine("Object stored at {0}", storedObjectResponse.Location);
            Console.ReadKey();
            Console.WriteLine("Deleting object stored at {0}", storedObjectResponse.Location);

            storageClient.StoredObject.DeleteObject(new DeleteObjectRequest
                                                                                    {
                                                                                        Resource = new Uri(storedObjectResponse.Location, UriKind.Relative)
                                                                                    });

            Console.WriteLine("Object at {0} deleted", storedObjectResponse.Location);
            Console.ReadKey();
        }
    }
}