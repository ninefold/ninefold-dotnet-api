using System;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.DemoClient
{
    class Program
    {
        static void Main(string[] args)
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

            var storageClient = new StorageClient("5cd104e23fc947668a6c74fe63fd77e7/godbold_1310683369246", "FGJeXUzxCz5poHoSzRzmMTceuek=");
            var storedObjectResponse = storageClient.StoredObject.CreateObject(new CreateObjectRequest
                                                                                   {
                                                                                       Base64Content = "some content for a file",
                                                                                       ContentLength = "some content for a file".Length,
                                                                                       ResourcePath ="/rest/objects",
                                                                                       GroupACL = "other=NONE",
                                                                                       ACL = "godbold=FULL_CONTROL",
                                                                                       Metadata = "part1=buy",
                                                                                       ListableMetadata = "part4/part7/part8=quick"
                                                                                   });
            Console.WriteLine("Object stored at {0}", storedObjectResponse.Location);

            Console.ReadKey();
        }
    }
}
