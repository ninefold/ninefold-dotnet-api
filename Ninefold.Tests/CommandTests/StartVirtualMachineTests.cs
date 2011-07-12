using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninefold.API.Compute.Commands;
using Ninefold.API.Tests.Fakes;
using RestSharp;

namespace Ninefold.API.Tests.CommandTests
{
    [TestClass]
    public class StartVirtualMachineTests
    {        
        [TestMethod]
        public void StartVirtualMachine_Execute_ShouldSendAPostRequestWhenExecuted()
        {
            //Arrange
            var stubService = new ComputeServiceStub();
            var command = new StartVirtualMachine(stubService, new byte[] { 0x0, 0x1 }, "1", "1");
            
            //Act
            command.Execute();
            
            //Assert
            Assert.AreEqual(Method.POST, stubService.Request.Method);
        }

        [TestMethod]
        public void StartVirtualMachine_Execute_ShouldSetCommandQueryStringParamToStartVirtualMachine()
        {
            //Arrange
            var stubService = new ComputeServiceStub();
            var command = new StartVirtualMachine(stubService, new byte[] { 0x0, 0x1 }, "1", "1");

            //Act
            command.Execute();

            //Assert
            Assert.AreEqual(Method.POST, stubService.Request.Method);
        }
        
        [TestMethod]
        public void StartVirtualMachine_Execute_ShouldContainMachineIdQueryStringParamWithValue()
        {
            //Arrange
            var stubService = new ComputeServiceStub();
            var command = new StartVirtualMachine(stubService, new byte[] { 0x0, 0x1 }, "1", "1");
            
            //Act
            command.Execute();

            //Assert
            Assert.IsTrue(stubService.Request.Parameters.Exists(p => p.Name == "machineid" && p.Type == ParameterType.UrlSegment), "No machineid URL parameter is associated with the request");
            Assert.AreEqual("1", stubService.Request.Parameters.Find(p => p.Name == "machineid").Value);
        }

        [TestMethod]
        public void StartVirtualMachine_Execute_ParametersShouldIncludeAnApiKey()
        {
            //Arrange
            var stubService = new ComputeServiceStub();
            var command = new StartVirtualMachine(stubService, new byte[] { 0x0, 0x1 }, "123", "1");
            
            //Act
            command.Execute();

            //Assert
            Assert.IsTrue(stubService.Request.Parameters.Exists(p => p.Name == "apikey" && p.Type == ParameterType.UrlSegment), "No apikey parameter is associated with the request");
            Assert.IsTrue(stubService.Request.Parameters.Find(p => p.Name == "apikey").Value.ToString() == "123");
        }

        [TestMethod]
        public void StartVirtualMachine_Execute_RequestParametersShouldNotIncludePlus()
        {
            //Arrange
            var stubService = new ComputeServiceStub();
            var command = new StartVirtualMachine(stubService, new byte[] { 0x0, 0x1 }, "1", "1");

            //Act   
            command.Execute();

            //Assert
            foreach (var param in stubService.Request.Parameters)
            {
                    Assert.IsFalse(param.Value.ToString().Contains("+"), string.Format("The parameter {0} contains a plus sign", param.Name));                
            }
        }

        [TestMethod]
        public void StartVirtualMachine_Execute_RequestParametersShouldBeAlphaOrdered()
        {
            //Arrange
            var stubService = new ComputeServiceStub();
            var command = new StartVirtualMachine(stubService, new byte[] { 0x0, 0x1 }, "1", "1");

            //Act
            command.Execute();

            //Assert
            AssertHelper.IsAlphaOrdered(stubService.Request.Parameters);
        }
    }
}
