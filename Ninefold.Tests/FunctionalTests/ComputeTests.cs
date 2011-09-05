using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninefold.Compute;
using Ninefold.Compute.Messages;

namespace Ninefold.API.Tests.FunctionalTests
{
    [TestClass]
    public class ComputeTests
    {
        [TestMethod]
        public void DeployVirtualMachine_ShouldAuthenticateCorrectly_ForValidRequest()
        {
            try
            {
                const string key = "5cd104e23fc947668a6c74fe63fd77e7/godbold_1310683369246";
                const string secret = "FGJeXUzxCz5poHoSzRzmMTceuek=";
                
                IComputeClient compute = new ComputeClient(key, secret);
                var response = compute.DeployVirtualMachine(new DeployVirtualMachineRequest
                                                 {
                                                     ServiceOfferingId = "someOffering",
                                                     TemplateId = "someTemplate",
                                                     ZoneId = "someZone"
                                                 });

                Assert.IsNotNull(response);
            }
            catch (Core.NinefoldApiException ex)
            {
                Assert.Fail(ex.ErrorMessage);
            }

            
        }
    }
}
