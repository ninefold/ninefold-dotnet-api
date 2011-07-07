using Ninefold.API.Core;
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
            var stubService = new ComputeServiceStub { Response = new ResponseStub() };
            var command = new StartVirtualMachine(stubService);
            
            //Act
            command.Execute();
            
            //Assert
            Assert.AreEqual(Method.POST, stubService.Request.Method);
        }

        [TestMethod]
        public void StartVirtualMachine_Execute_ShouldSetCommandQueryStringParamToStartVirtualMachine()
        {
            //Arrange
            var stubService = new ComputeServiceStub { Response = new ResponseStub() };
            var command = new StartVirtualMachine(stubService);

            //Act
            command.Execute();

            //Assert
            Assert.IsTrue(stubService.Request.Parameters.Exists(p => p.Name == "command" && p.Type == ParameterType.UrlSegment), "No command URL parameter is associated with the request");
            Assert.AreEqual("startvirtualmachine", stubService.Request.Parameters.Find(p => p.Name == "command").Value);
        }
    }
}
