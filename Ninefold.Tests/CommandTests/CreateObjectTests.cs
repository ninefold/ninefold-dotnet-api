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


    }
}
