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
            var command = new CreateObject(stubService, new byte[] { 0x0, 0x1 })
            {
                Content = new byte[] { 0x1, 0x0 },
                ResourcePath = "objects/seattle/sun.jpg"
            };

            command.Execute();

            Assert.AreEqual(Method.POST, stubService.Request.Method);
        }

        [TestMethod]
        public void CreateObject_Execute_ShouldCreateRequestWithObjectsURL()
        {
            var stubService = new StorageServiceStub();
            var command = new CreateObject(stubService, new byte[] {0x0, 0x1})
                              {
                                  Content = new byte[] {0x1, 0x0},
                                  ResourcePath = "objects/seattle/sun.jpg"
                              };

            command.Execute();

            Assert.IsTrue(stubService.Uri.ToString().Contains("objects/seattle/sun.jpg"), "Object url not contained in request");
        }

        [TestMethod]
        public void CreateObject_Execute_ShouldCreateRequestWithOneOfTheDateHeaders()
        {
            var stubService = new StorageServiceStub();
            var command = new CreateObject(stubService, new byte[] { 0x0, 0x1 })
            {
                Content = new byte[] { 0x1, 0x0 },
                ResourcePath = "objects/seattle/sun.jpg"
            };

            command.Execute();
            
            var dateHeader =
                stubService.Request.Parameters.FirstOrDefault(
                    p => p.Name.Equals("x-emc-date", StringComparison.InvariantCultureIgnoreCase)
                                && p.Type == ParameterType.HttpHeader) ??
                stubService.Request.Parameters.FirstOrDefault(
                        p => p.Name.Equals("date", StringComparison.InvariantCultureIgnoreCase)
                                && p.Type == ParameterType.HttpHeader);

            Assert.IsNotNull(dateHeader, "Date parameter not found on request");
        }

        [TestMethod]
        public void CreateObject_Execute_ShouldCreateASignedRequest()
        {
            var stubService = new StorageServiceStub();
            var command = new CreateObject(stubService, new byte[] { 0x0, 0x1 })
            {
                Content = new byte[] { 0x1, 0x0 },
                ResourcePath = "objects/seattle/sun.jpg"
            };

            command.Execute();

            Assert.IsNotNull(stubService.Request.Parameters.FirstOrDefault(p => p.Name.Equals("x-emc-signature") && p.Type == ParameterType.HttpHeader));
        }
    }
}
