using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninefold.API.Compute;
using Ninefold.API.Compute.Commands;
using Ninefold.API.Compute.Messages;
using Ninefold.API.Core;
using Ninefold.API.Tests.Fakes;
using RestSharp;

namespace Ninefold.API.Tests.CommandTests
{
    [TestClass]
    public class StartVirtualMachineTests
    {        
        [TestMethod]
        public void Test1()
        {
            var requestBuilder = new ComputeRequestBuilder();
            var request = requestBuilder.GenerateRequest(new DeployVirtualMachineRequest
                                                             {
                                                                 ServiceOfferingId = "service offering",
                                                                 TemplateId = "template",
                                                                 ZoneId = "zone id"
                                                             }, "apikey value");
            var client = new RestClient("http://tempuri.org");
            var uri = client.BuildUri((RestRequest)request);
            
            Assert.AreEqual(string.Empty, uri.ToString());
        }
    }
}
