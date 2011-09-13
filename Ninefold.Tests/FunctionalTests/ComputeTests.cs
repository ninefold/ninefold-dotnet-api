using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninefold.Compute;
using Ninefold.Compute.Commands;
using Ninefold.Compute.Messages;
using Ninefold.Core;

namespace Ninefold.API.Tests.FunctionalTests
{
    [TestClass]
    public class ComputeTests
    {
        IComputeClient _compute;

        [TestInitialize]
        public void TestInitialise()
        {
            var credentialsFilePath = Path.Combine(Environment.CurrentDirectory, "credentials.csv");

            if (!File.Exists(credentialsFilePath))
            {
                throw new FileNotFoundException(
                    string.Format("Could not locate the credentials file required for the functional tests. Expected at {0}", credentialsFilePath), credentialsFilePath);
            }
            
            var credentialsFileContent = File.ReadAllText(credentialsFilePath).Trim();
            var credentials = credentialsFileContent.Split(',');
            var apiKey = credentials[0].Trim();

            var secret = credentials[1];
            secret = secret.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
            secret = Convert.ToBase64String(Encoding.Default.GetBytes(secret));

            _compute = new ComputeClient(apiKey, secret);
        }

        [TestMethod]
        public void ListVirtualMachines()
        {
            try
            {
                var response = _compute.ListVirtualMachines(new ListVirtualMachinesRequest());

                Assert.IsNotNull(response);
                Assert.IsTrue(response.Machines.Count() > 0);
                Assert.IsFalse(string.IsNullOrWhiteSpace(response.Machines.ElementAt(0).Name));
            }
            catch (NinefoldApiException ex)
            {
                Assert.Fail("Ninefold Exception thrown: {0}", ex.ErrorMessage);
            }

        }

        [TestMethod]
        public void ListTemplates()
        {
            try
            {
                var response = _compute.ListTemplates(new ListTemplatesRequest { TemplateFilter = "featured" });

                Assert.IsNotNull(response);
                Assert.IsTrue(response.Templates.Count() > 0);
                Assert.IsFalse(string.IsNullOrWhiteSpace(response.Templates.ElementAt(0).Name));
            }
            catch (NinefoldApiException ex)
            {
                Assert.Fail("Ninefold Exception thrown: {0}", ex.ErrorMessage);
            }
        }

        [TestMethod]
        public void ListAccounts()
        {
            try
            {
                var response = _compute.ListAccounts(new ListAccountsRequest());

                Assert.IsNotNull(response);
                Assert.IsTrue(response.Accounts.Count() > 0);
            }
            catch (NinefoldApiException ex)
            {
                Assert.Fail("Ninefold Exception thrown: {0}", ex.ErrorMessage);
            }
        }

        [TestMethod]
        public void ListServiceOfferings()
        {
            try
            {
                var response = _compute.ListServiceOfferings(new ListServiceOfferingsRequest());

                Assert.IsNotNull(response);
                Assert.IsTrue(response.ServiceOfferings.Count() > 0);
                Assert.IsFalse(string.IsNullOrWhiteSpace(response.ServiceOfferings.ElementAt(0).Name));
            }
            catch (NinefoldApiException ex)
            {
                Assert.Fail("Ninefold Exception thrown: {0}", ex.ErrorMessage);
            }
        }

        [TestMethod]
        public void ListTemplatePermissions()
        {
            try
            {
                var response = _compute.ListTemplatePermissions(new ListTemplatePermissionsRequest { Id = "425", });

                Assert.IsNotNull(response);
                Assert.AreEqual(425, response.Id);
            }
            catch (NinefoldApiException ex)
            {
                Assert.Fail("Ninefold Exception thrown: {0}", ex.ErrorMessage);
            }
        }

        [TestMethod]
        public void ListIsos()
        {
            try
            {
                var response = _compute.ListIsos(new ListIsosRequest { Id = "494" });
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Isos.Count());
            }
            catch (NinefoldApiException ex)
            {
                Assert.Fail("Ninefold Exception thrown: {0}", ex.ErrorMessage);
            }
        }

        [TestMethod]
        public void ListIsoPermisssions()
        {
            try
            {
                var response = _compute.ListIsoPermissions(new ListIsoPermissionsRequest { Id = "494" });
                Assert.IsNotNull(response);
                Assert.AreNotEqual(0, response.Id);
            }
            catch (NinefoldApiException ex)
            {
                Assert.Fail("Ninefold Exception thrown: {0}", ex.ErrorMessage);
            }
        }
    }
}
