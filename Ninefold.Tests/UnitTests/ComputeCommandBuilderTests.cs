using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninefold.API.Tests.UnitTests.Stub;
using Ninefold.Compute;
using NSubstitute;

namespace Ninefold.API.Tests.UnitTests
{
    [TestClass]
    public class ComputeCommandBuilderTests
    {
        [TestMethod]
        [TestCategory("unit")]
        public void CommandBuilder_GenerateRequest_ShouldReturnGetRequest()
        {
            var commandBuilder = new ComputeCommandBuilder();

            var request = commandBuilder.GenerateRequest(new StubRequest(),
                                                                    Substitute.For<IComputeCommandAuthenticator>(),
                                                                    "http://tempuri.org",
                                                                    "key",
                                                                    "AAAAAAAAAAAAAAAA");

            Assert.IsNotNull(request);
            Assert.AreEqual("GET", request.Method);
        }

        [TestMethod]
        [TestCategory("unit")]
        public void CommandBuilder_GenerateRequest_ShouldReturnQueryContainingAllProvidedRequestParams()
        {
            var commandBuilder = new ComputeCommandBuilder();

            var request = commandBuilder.GenerateRequest(new StubRequest
                                                             {
                                                                 ParameterOne = "1",
                                                                 ParameterTwo = "2",
                                                             },
                                                                    Substitute.For<IComputeCommandAuthenticator>(),
                                                                    "http://tempuri.org",
                                                                    "key",
                                                                    "AAAAAAAAAAAAAAAA");

            Assert.IsNotNull(request);
            Assert.IsTrue(request.RequestUri.Query.Contains("parameterOne=1"));
            Assert.IsTrue(request.RequestUri.Query.Contains("parameterTwo=2"));
        }

        [TestMethod]
        [TestCategory("unit")]
        public void CommandBuilder_GenerateRequest_ShouldNotIncludeAnEmptyParameter()
        {
            var commandBuilder = new ComputeCommandBuilder();

            var request = commandBuilder.GenerateRequest(new StubRequest
            {
                ParameterOne = "1",
            },
                                                                    new ComputeCommandAuthenticator(),
                                                                    "http://tempuri.org",
                                                                    "key",
                                                                    "AAAAAAAAAAAAAAAA");

            Assert.IsNotNull(request);
            Assert.IsFalse(request.RequestUri.Query.Contains("parameterTwo=2"));
        }

        [TestMethod]
        [TestCategory("unit")]
        public void CommandBuilder_GenerateRequest_CallsAuthenticationForSignature()
        {
            var commandBuilder = new ComputeCommandBuilder();
            var mockAuthenticator = Substitute.For<IComputeCommandAuthenticator>();

            commandBuilder.GenerateRequest(new StubRequest(), mockAuthenticator, "http://tempuri.com", "", "");

            mockAuthenticator.Received().AuthenticateRequest(Arg.Any<string>(), Arg.Any<string>());
        }

        [TestMethod]
        [TestCategory("unit")]
        public void CommandBuilder_GenerateRequest_AppendsSignatureToRequest()
        {
            const string secret = "AAAAAAAAAAAAAAAA";

            var commandBuilder = new ComputeCommandBuilder();            
            var authenticator =Substitute.For<IComputeCommandAuthenticator>();
            authenticator.AuthenticateRequest(Arg.Any<string>(), secret).Returns("signature");
                                                                    

            var request = commandBuilder.GenerateRequest(new StubRequest
                                                                    {
                                                                        ParameterOne = "1",
                                                                    },
                                                                    authenticator,
                                                                    "http://tempuri.org",
                                                                    "key",
                                                                    secret);

            Assert.IsNotNull(request);
            Assert.IsTrue(request.RequestUri.Query.EndsWith("&signature=signature"), string.Format("Query ends with {0}", request.RequestUri.Query));
        }

        [TestMethod]
        [TestCategory("unit")]
        public void CommandBuilder_GenerateRequest_ParameterNamesAreCamelCased()
        {
            var commandBuilder = new ComputeCommandBuilder();
            var authenticator = Substitute.For<IComputeCommandAuthenticator>();

            var request = commandBuilder.GenerateRequest(new StubRequest
            {
                ParameterOne = "1",
            },
                                                                   authenticator,
                                                                   "http://tempuri.org",
                                                                   "key",
                                                                   "AAAAAAAAAA");

            Assert.IsNotNull(request);
            Assert.IsTrue(request.RequestUri.Query.Contains("parameterOne"));
        }

        [TestMethod]
        [TestCategory("unit")]
        public void CommandBuilder_GenerateRequest_QueryStartsWithApiKeyAndCommand()
        {
            var commandBuilder = new ComputeCommandBuilder();
            var authenticator = Substitute.For<IComputeCommandAuthenticator>();

            var request = commandBuilder.GenerateRequest(new StubRequest
            {
                ParameterOne = "1",
            },
                                                                   authenticator,
                                                                   "http://tempuri.org",
                                                                   "key",
                                                                   "AAAAAAAAAA");

            Assert.IsNotNull(request);
            Assert.IsTrue(request.RequestUri.Query.StartsWith("?apiKey=key&command=stubRequest"));
        }
    }
}