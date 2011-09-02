using System;
using System.IO;
using System.Text;
using Ninefold.Storage;
using Ninefold.Storage.Messages;

namespace Ninefold.API.DemoClient
{
    class Program
    {
        static void Main()
        {
            var demoContent = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "DemoFile.txt"));
            var storageClient = new StorageClient("5cd104e23fc947668a6c74fe63fd77e7/godbold_1310683369246",
                                                  "FGJeXUzxCz5poHoSzRzmMTceuek=");
            var storedObjectResponse = storageClient.CreateObject(new CreateObjectRequest
                                                                                   {
                                                                                       Content = demoContent,
                                                                                       Resource =
                                                                                           new Uri("objects", UriKind.Relative),
                                                                                       GroupACL = "other=NONE",
                                                                                       ACL = "godbold=FULL_CONTROL",
                                                                                       Metadata = "part1=buy",
                                                                                       ListableMetadata =
                                                                                           "part4/part7/part8=quick"
                                                                                   });

            Console.WriteLine("Object stored at {0}", storedObjectResponse.Location);
            Console.ReadKey();

            var namespaceCreateResponse = storageClient.CreateObject(new CreateObjectRequest
            {
                Resource =
                    new Uri("namespace/test/profiles of stuff/", UriKind.Relative),
                GroupACL = "other=NONE",
                ACL = "godbold=FULL_CONTROL",
                Metadata = "part1=buy",
                ListableMetadata =
                    "part4/part7/part8=quick"
            });

            Console.WriteLine("Namespace created at {0}", namespaceCreateResponse.Location);
            Console.ReadKey();

            var getNamespaceResponse =
                storageClient.GetObject(new GetObjectRequest
                {
                    Resource = new Uri(namespaceCreateResponse.Location, UriKind.Relative),
                });

            var namespaceContent = Encoding.ASCII.GetChars(getNamespaceResponse.Content, 0, getNamespaceResponse.Content.Length);
            var namespaceContentString = new string(namespaceContent);

            Console.WriteLine("Namespace {0} retrieved", namespaceCreateResponse.Location);
            Console.WriteLine("Content: {0}", namespaceContentString);
            Console.ReadKey(); 

            Console.WriteLine("Deleting namespace at {0}", namespaceCreateResponse.Location);

            storageClient.DeleteObject(new DeleteObjectRequest
            {
                Resource = new Uri(namespaceCreateResponse.Location, UriKind.Relative)
            });

            Console.WriteLine("Namespace at {0} deleted", namespaceCreateResponse.Location);
            Console.ReadKey();


            var getFullObjectResponse =
                storageClient.GetObject(new GetObjectRequest
                {
                    Resource = new Uri(storedObjectResponse.Location, UriKind.Relative),
                });

            var allContent = Encoding.ASCII.GetChars(getFullObjectResponse.Content, 0, getFullObjectResponse.Content.Length);
            var allContentString = new string(allContent);

            Console.WriteLine("Object {0} retrieved", storedObjectResponse.Location);
            Console.WriteLine("Content: {0}", allContentString);
            Console.ReadKey(); 

            var getObjectResponse =
                storageClient.GetObject(new GetObjectRequest
                                                         {
                                                             Resource = new Uri(storedObjectResponse.Location, UriKind.Relative),
                                                             LowerRange = 10
                                                         });

            var content = Encoding.ASCII.GetChars(getObjectResponse.Content, 0, getObjectResponse.Content.Length);
            var contentString = new string(content);

            Console.WriteLine("Object {0} retrieved", storedObjectResponse.Location);
            Console.WriteLine("Content: {0}", contentString);
            Console.ReadKey();

            Console.WriteLine("Updating object stored at {0}", storedObjectResponse.Location);

            storageClient.UpdateObject(new UpdateObjectRequest
            {
                Resource = new Uri(storedObjectResponse.Location, UriKind.Relative),
                Content = demoContent
            });

            Console.WriteLine("Object at {0} was updated", storedObjectResponse.Location);
            Console.ReadKey();

            Console.WriteLine("Deleting object stored at {0}", storedObjectResponse.Location);

            storageClient.DeleteObject(new DeleteObjectRequest
                                                                                    {
                                                                                        Resource = new Uri(storedObjectResponse.Location, UriKind.Relative)
                                                                                    });

            Console.WriteLine("Object at {0} deleted", storedObjectResponse.Location);
            Console.ReadKey();
        }
    }
}