using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninefold.API.Storage.Commands;
using Ninefold.API.Tests.Fakes;
using RestSharp;

namespace Ninefold.API.Tests.CommandTests
{
    [TestClass]
    public class CreateObjectTests
    {
        [TestMethod]
        public void CreateObject_Execute_ShouldCreatePostRequest()
        {
            var stubService = new StorageServiceStub();
            var command = new CreateObject(stubService);
            
            command.Execute();

            Assert.AreEqual(Method.POST, stubService.Request.Method);
        }

        [TestMethod]
        public void CreateObject_Execute_ShouldCreateRequestWithObjectsURL()
        {
            var stubService = new StorageServiceStub();
            var command = new CreateObject(stubService);

            command.Execute();

            Assert.IsTrue(stubService.Uri.ToString().Contains("rest/objects"), "Url is not using the objects path");
        }

        [TestMethod]
        public void CreateObject_Execute_ShouldCreateRequestWithDateHeader()
        {
            var stubService = new StorageServiceStub();
            var command = new CreateObject(stubService);

            command.Execute();
            
            var x =
                stubService.Headers.FirstOrDefault(
                    p => p.Name.Equals("date", StringComparison.InvariantCultureIgnoreCase));

            Assert.IsNotNull(x, "Date parameter not found on request");
        }
    }
}
