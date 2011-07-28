using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninefold.API.Core;
using Ninefold.API.Storage;
using Ninefold.API.Storage.Messages;

namespace Ninefold.API.Tests.AuthenticationTests
{
    [TestClass]
    public class StorageAuthenticatorTests
    {
        [TestMethod]
        public void Authenticate_ShouldAddHeader()
        {
            const string secret = "LJLuryj6zs8ste6Y3jTGQp71xq0=";
            const string expectedSig = "WHJo1MFevMnK4jCthJ974L3YHoo=";

            var request = WebRequest.Create("http://onlinestorage.ninefold.com/rest/objects");
            request.Method = "POST";
            request.ContentType = "application/octet-stream";
            request.Headers.Add("x-emc-groupacl", "other=NONE");
            request.Headers.Add("x-emc-date", "Thu, 05 Jun 2008 16:38:19 GMT");
            request.Headers.Add("x-emc-listable-meta", "part4/part7/part8=quick");
            request.Headers.Add("x-emc-meta", "part1=buy");
            request.Headers.Add("x-emc-uid", "6039ac182f194e15b9261d73ce044939/user1");
            request.Headers.Add("x-emc-useracl", "john=FULL_CONTROL,mary=WRITE");

            var signatureService = new StorageCommandAuthenticator();
            signatureService.AuthenticateRequest(request, secret);

            Assert.AreEqual(expectedSig, "");
        }

        [TestMethod]
        public void SignatureTest()
        {
            //Arrange
            var createRequest = new CreateObjectRequest
            {
                Content = new byte[] { 0x0, 0x1, 0x2},
                Resource = new Uri("/rest/object"),
                GroupACL = "other=NONE",
                ACL = "godbold=FULL_CONTROL",
                Metadata = "part1=buy",
                ListableMetadata = "part4/part7/part8=quick"
            };

            var requestBuilder = new StorageCommandBuilder();
            requestBuilder.GenerateRequest(createRequest, "", HttpMethod.POST);
        }
    }
}
