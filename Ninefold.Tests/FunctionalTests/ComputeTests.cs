using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninefold.Compute;
using Ninefold.Compute.Messages;
using Ninefold.Core;

namespace Ninefold.API.Tests.FunctionalTests
{
    [TestClass]
    public class ComputeTests
    {
        string _apiKey;
        string _secret;

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
            _apiKey = credentials[0].Trim();

            var secret = credentials[1];
            secret = secret.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
            _secret = Convert.ToBase64String(Encoding.Default.GetBytes(secret));
        }

        [TestMethod]
        public void ListTemplates()
        {
            try
            {
                var compute = new ComputeClient(_apiKey, _secret);
                var response = compute.ListTemplates(new ListTemplatesRequest { TemplateFilter = "featured" });

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
                var compute = new ComputeClient(_apiKey, _secret);
                var response = compute.ListAccounts(new ListAccountsRequest());

                Assert.IsNotNull(response);
                Assert.IsTrue(response.Accounts.Count() > 0);
            }
            catch (NinefoldApiException ex)
            {
                Assert.Fail("Ninefold Exception thrown: {0}", ex.ErrorMessage);
            }
        }
    }
}
