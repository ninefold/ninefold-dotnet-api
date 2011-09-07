using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninefold.Compute;
using Ninefold.Compute.Messages;
using Ninefold.Core;

namespace Ninefold.API.Tests.FunctionalTests
{
    [TestClass]
    public class ComputeTests
    {
        private const string Secret = "";
        private const string ApiKey = "";

        static readonly string Base64Secret = Convert.ToBase64String(Encoding.Default.GetBytes(Secret));

        [TestMethod]
        public void ListTemplates_ShouldListFeaturedTemplates_ForFeatureFilter()
        {
            try
            {
                var compute = new ComputeClient(ApiKey, Base64Secret);
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
    }
}
