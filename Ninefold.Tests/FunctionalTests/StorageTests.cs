using System;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninefold.Core;
using Ninefold.Storage;
using Ninefold.Storage.Messages;

namespace Ninefold.API.Tests.FunctionalTests
{
    [TestClass]
    public class StorageTests
    {
        byte[] _demoContent;
        StorageClient _storageClient;
        string _objectId;

        [TestInitialize]
        public void TestSetup()
        {
            _demoContent = Encoding.ASCII.GetBytes("Some text from a file");
            _storageClient = new StorageClient("5cd104e23fc947668a6c74fe63fd77e7/godbold_1310683369246",
                                                  "FGJeXUzxCz5poHoSzRzmMTceuek=");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (!string.IsNullOrWhiteSpace(_objectId))
            {
                DeleteLastObject();
            }

            _objectId = string.Empty;
        }

        private void CreateObject(string tags="part7/part8=quick")
        {
            var response = _storageClient.CreateObject(new CreateObjectRequest
            {
                Content = _demoContent,
                Resource = new Uri("objects", UriKind.Relative),
                GroupACL = "other=NONE",
                ACL = "godbold=FULL_CONTROL",
                Metadata = "part1=buy, part4=someData",
                ListableMetadata = tags
            });

            _objectId = response.Location;
        }

        private GetObjectResponse GetObject()
        {
            return _storageClient.GetObject(new GetObjectRequest
            {
                IncludeMeta = true,
                Resource = new Uri(_objectId, UriKind.Relative)
            });
        }

        private void DeleteLastObject()
        {
            _storageClient.DeleteObject(new DeleteObjectRequest
            {
                Resource = new Uri(_objectId, UriKind.Relative)
            });
        }

        private void CreateNamespace(string path)
        {
            var response = _storageClient.CreateObject(new CreateObjectRequest
            {
                Resource = new Uri(path, UriKind.Relative),
                GroupACL = "other=NONE",
                ACL = "godbold=FULL_CONTROL",
                Metadata = "part1=buy",
                ListableMetadata = "part4/part7/part8=quick"
            });

            _objectId = response.Location;
        }

        [TestMethod]
        public void CreateObject_ShouldStoreObject_OnValidRequest()
        {
            var response = _storageClient.CreateObject(new CreateObjectRequest
             {
                 Content = _demoContent,
                 Resource =
                     new Uri("objects", UriKind.Relative),
                 GroupACL = "other=NONE",
                 ACL = "godbold=FULL_CONTROL",
                 Metadata = "part1=buy",
                 ListableMetadata =
                     "part4/part7/part8=quick"
             });

            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Location));
        }

        [TestMethod]
        public void CreateObject_ShouldCreateNamespace_ForNSWithSpaceInName()
        {
            var response = _storageClient.CreateObject(new CreateObjectRequest
            {
                Resource =
                    new Uri("namespace/test/profiles and stuff/", UriKind.Relative),
                GroupACL = "other=NONE",
                ACL = "godbold=FULL_CONTROL",
                Metadata = "part1=buy",
                ListableMetadata =
                    "part4/part7/part8=quick"
            });
            _objectId = response.Location;
            Assert.IsFalse(string.IsNullOrWhiteSpace(_objectId));
        }

        [TestMethod]
        public void CreateObject_ShouldCreateNamespace_ForNSWithoutSpaces()
        {
            var response = _storageClient.CreateObject(new CreateObjectRequest
            {
                Resource =
                    new Uri("namespace/tests/", UriKind.Relative),
                GroupACL = "other=NONE",
                ACL = "godbold=FULL_CONTROL",
                Metadata = "part1=buy",
                ListableMetadata =
                    "part4/part7/part8=quick"
            });
            _objectId = response.Location;

            Assert.IsFalse(string.IsNullOrWhiteSpace(_objectId));
        }

        [TestMethod]
        public void GetObject_ShouldReturnObjectText_ForGetStoredObject()
        {
            CreateObject();
            var expectedContent = Encoding.ASCII.GetString(_demoContent);
            var response = _storageClient.GetObject(new GetObjectRequest
            {
                Resource = new Uri(_objectId, UriKind.Relative)
            });
            var actualContent = Encoding.ASCII.GetString(response.Content);

            Assert.AreEqual(expectedContent, actualContent);
        }

        [TestMethod]
        public void GetObject_ShouldReturnBytes10ToEnd_ForRangeRequestWithLowerBoundOf10()
        {
            CreateObject();

            var expectedContent = Encoding.ASCII.GetString(_demoContent).Substring(10);
            var response =
                _storageClient.GetObject(new GetObjectRequest
                {
                    Resource = new Uri(_objectId, UriKind.Relative),
                    LowerRange = 10
                });

            var actualContent = Encoding.ASCII.GetString(response.Content);

            Assert.AreEqual(expectedContent, actualContent);
        }

        [TestMethod]
        public void GetObject_ShouldReturnBytes5To8_ForRangeRequestWithLowerOf5AndUpperOf8()
        {
            CreateObject();

            var expectedContent = Encoding.ASCII.GetString(new[] { _demoContent[5], _demoContent[6], _demoContent[7], _demoContent[8] });
            var response =
                _storageClient.GetObject(new GetObjectRequest
                {
                    Resource = new Uri(_objectId, UriKind.Relative),
                    LowerRange = 5,
                    UpperRange = 8
                });
            var actualContent = Encoding.ASCII.GetString(response.Content);

            Assert.AreEqual(expectedContent, actualContent);
        }

        [TestMethod]
        public void GetObject_ShouldReturnNamespaceListing_ForGetNamespaceWithNoSpaces()
        {
            CreateNamespace("namespace/testnamespace/");
            var response =
                _storageClient.ListNamespace(new ListNamespaceRequest
                {
                    Resource = new Uri(_objectId, UriKind.Relative),
                });

            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.Root != null);
            Assert.IsTrue(response.Content.Root.DescendantNodes().Count() > 0, "No  nodes returned");
        }

        [TestMethod]
        public void GetObject_ShouldReturnNamespaceListing_ForGetNamespaceWithSpaces()
        {
            CreateNamespace("namespace/test namespace/");
            var response =
                _storageClient.ListNamespace(new ListNamespaceRequest
                {
                    Resource = new Uri(_objectId, UriKind.Relative),
                });

            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.Root != null);
            Assert.IsTrue(response.Content.Root.DescendantNodes().Count() > 0, "No nodes returned");
        }

        [TestMethod]
        public void DeleteObject_ShouldDeleteNamespace_ForValidPath()
        {
            CreateNamespace("namespace/left/right/");
            var response = _storageClient.DeleteObject(new DeleteObjectRequest
            {
                Resource = new Uri(_objectId, UriKind.Relative)
            });
            _objectId = string.Empty;

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void UpdateObject_ShouldUpdateExistingObject_ToNewContent()
        {
            const string updateText = "Updated text for a file";

            CreateObject();

            var updateContent = Encoding.ASCII.GetBytes(updateText);
            var response = _storageClient.UpdateObject(new UpdateObjectRequest
            {
                Resource = new Uri(_objectId, UriKind.Relative),
                Content = updateContent
            });

            var updatedObject = GetObject();
            
            Assert.IsTrue(response.Delta != 0);
            Assert.AreEqual(updateText, Encoding.ASCII.GetString(updatedObject.Content));
        }

        [TestMethod]
        public void DeleteObject_ShouldDeleteObject_ForValidRequest()
        {
            CreateObject();

            var response = _storageClient.DeleteObject(new DeleteObjectRequest
                                                                        {
                                                                            Resource =
                                                                                new Uri(_objectId, UriKind.Relative)
                                                                        });
            _objectId = string.Empty;
            Assert.IsTrue(response.Delta != 0);
        }

        [TestMethod]
        public void SetACL_ShouldSetUserACLOnExistingObject_ForValidRequest()
        {
            CreateObject();

            _storageClient.SetObjectACL(new SetObjectACLRequest
                {
                    Resource = new Uri(_objectId, UriKind.Relative),
                    UserACL = "godbold=FULL_CONTROL, somone=READ"
                });
        }

        [TestMethod]
        public void SetACL_ShouldSetGroupACLOnExistingObject_ForValidRequest()
        {
            CreateObject();

            _storageClient.SetObjectACL(new SetObjectACLRequest
            {
                Resource = new Uri(_objectId, UriKind.Relative),
                GroupACL = "other=FULL_CONTROL"
            });
        }

        [TestMethod]
        public void SetACL_ShouldFail_ForNoACLEntriesProvided()
        {
            var exceptionCaught = false;
            CreateObject();

            try
            {
                _storageClient.SetObjectACL(new SetObjectACLRequest
                                                             {
                                                                 Resource = new Uri(_objectId, UriKind.Relative),
                                                             });
            }
            catch (ArgumentOutOfRangeException)
            {
                exceptionCaught = true;
            }

            Assert.IsTrue(exceptionCaught);
        }

        [TestMethod]
        public void DeleteUserMetadata_ShouldRemoveMetadata_OnUserObject()
        {
            CreateObject();
            var checkObject = GetObject();
            
            _storageClient.DeleteUserMetadata(new DeleteUserMetadataRequest
                                {
                                    Resource = new Uri(_objectId, UriKind.Relative),
                                    Tags = "part4"
                                });

            var savedObject = GetObject();
            Assert.IsTrue(checkObject.Metadata.Contains("part4"));
            Assert.IsFalse(savedObject.Metadata.Contains("part4"));
        }

        [TestMethod]
        public void DeleteUserMetadata_ShouldReturnNinefoldExceptionWithMessage_OnBrokenRequest()
        {
            NinefoldApiException apiException = null;

            try
            {
                _storageClient.DeleteUserMetadata(new DeleteUserMetadataRequest
                {
                    Resource = new Uri("someId", UriKind.Relative),
                    Tags = "22"
                });
            }
            catch (NinefoldApiException ex)
            {
                apiException = ex;
                Assert.IsFalse(string.IsNullOrWhiteSpace(ex.Message), "No custom exception message was returned");
                Assert.IsFalse(string.IsNullOrWhiteSpace(ex.Code), "No custom exception code was returned");                
            }

            Assert.IsNotNull(apiException, "No api exception was caught");
        }

        [TestMethod]
        public void GetObjectAcl_ShouldReturnValidAcls_ForValidRequest()
        {
            CreateObject();
            var createdObject = GetObject();

            var response = _storageClient.GetObjectACL(new GetObjectAclRequest
                                                                        {
                                                                            Resource = new Uri(_objectId, UriKind.Relative)
                                                                        });
            
            Assert.AreEqual(createdObject.UserAcl, response.UserAcl);
            Assert.AreEqual(createdObject.GroupAcl, response.GroupAcl);
        }

        [TestMethod]
        public void GetListableTags_ShouldReturnAllListableTags_ForValidObjectRequest()
        {
            var response = _storageClient.GetListableTags(new GetListableTagsRequest
                                                                           {
                                                                               Resource = new Uri("objects", UriKind.Relative)
                                                                           });

            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Tags));
        }

        [TestMethod]
        public void GetListableTags_ShouldReturnAllListableTags_ForValidNamespaceRequest()
        {
            var response = _storageClient.GetListableTags(new GetListableTagsRequest
            {
                Resource = new Uri("namespace", UriKind.Relative)
            });

            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Tags));
        }

        [TestMethod]
        public void GetSystemMetadata_ShouldReturnSystemMetadata_ForObject()
        {
            CreateObject();

            var response = _storageClient.GetSystemMetadata(new GetSystemMetadataRequest
                                                                             {
                                                                                 Resource =
                                                                                     new Uri(_objectId, UriKind.Relative)
                                                                             });

            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Metadata));    
        }

        [TestMethod]
        public void GetSystemMetadata_ShouldReturnOnlyRequestedSystemMetadata_ForFilteredObjectRequest()
        {
            CreateObject();

            var response = _storageClient.GetSystemMetadata(new GetSystemMetadataRequest
            {
                Resource = new Uri(_objectId, UriKind.Relative),
                Tags = "atime"
            });

            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Metadata));            
            Assert.AreEqual(1, response.Metadata.ToCharArray().Count(c => c == '='), "More than one key value pair was found in a filtered request");
            Assert.IsTrue(response.Metadata.Contains("atime"), "The requested key pair was not present in the metadata returned");
        }

        [TestMethod]
        public void ListObjects_ShouldReturnContent_ForValidRequest()
        {
            var response = _storageClient.ListObjects(new ListObjectsRequest
                                                                       {
                                                                           IncludeMetadata = 1,
                                                                           Tags = "part4/part7/part8",
                                                                           Resource = new Uri("objects", UriKind.Relative)
                                                                       });

            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.Root != null);
            Assert.IsTrue(response.Content.Root.Elements().Count() > 0, "No object nodes returned");
        }

        [TestMethod]
        public void ListObjects_ShouldReturnOneObjectAndAToken_ForRequestWithLimitSpecifiedAsOne()
        {
            var response = _storageClient.ListObjects(new ListObjectsRequest
            {
                IncludeMetadata = 1,
                Tags = "part4/part7/part8",
                MaxReturnCount = 1,
                Resource = new Uri("objects", UriKind.Relative)
            });

            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.Root != null);
            Assert.AreEqual(1, response.Content.Root.Elements().Count());
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Token));
        }

        [TestMethod]
        public void ListObjects_ShouldReturnTheNextObjectAndAToken_ForRequestsWithAToken()
        {
            var firstResponse = _storageClient.ListObjects(new ListObjectsRequest
            {
                IncludeMetadata = 1,
                Tags = "part4/part7/part8",
                MaxReturnCount = 1,
                Resource = new Uri("objects", UriKind.Relative)
            });

            var response = _storageClient.ListObjects(new ListObjectsRequest
            {
                IncludeMetadata = 1,
                Tags = "part4/part7/part8",
                MaxReturnCount = 1,
                Resource = new Uri("objects", UriKind.Relative)
            });

            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.Root != null);
            Assert.IsTrue(firstResponse.Content.Root != null);
            Assert.AreNotEqual(firstResponse.Content.Root.Elements().First(), response.Content.Root.Elements().First());
            Assert.AreEqual(1, response.Content.Root.Elements().Count());
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.Token));
        }

        [TestMethod]
        public void GetUserMetadata_ShouldReturnCorrectMetadata_ForValidRequest()
        {
            CreateObject();
            var referenceObject = GetObject();
            var response = _storageClient.GetUserMetadata(new GetUserMetadataRequest
                                                                           {
                                                                               Resource = new Uri(_objectId, UriKind.Relative)
                                                                           });

            var tagKeyValuePairs = referenceObject.ListableTags.Split(',');
            var listableTags = response.ListableTags.Split(',');

            foreach (var tagKeyValuePair in tagKeyValuePairs)
            {
                var currentTag = tagKeyValuePair.Substring(0, tagKeyValuePair.IndexOf('='));
                Assert.IsTrue(listableTags.Any(tag => tag.Equals(currentTag)), string.Format("Tag {0} not found", currentTag));
            }
        }

        [TestMethod]
        public void SetUserMetadata_ShouldUpdateObjectListableTags_ForValidRequest()
        {
            CreateObject(string.Empty);

            const string tags = "tag22=one, tag44=two";
            _storageClient.SetUserMetadata(new SetUserMetadataRequest
                                                            {
                                                                Resource = new Uri(_objectId, UriKind.Relative),
                                                                ListableTags = tags
                                                            });

            var referenceObject = GetObject();

            Assert.AreEqual(tags, referenceObject.ListableTags);
        }

        [TestMethod]
        public void SetUserMetadata_ShouldUpdateObjectTags_ForValidRequest()
        {
            CreateObject();

            const string tags = "tag22=one, tag44=two";
            _storageClient.SetUserMetadata(new SetUserMetadataRequest
                                                            {
                                                                Resource = new Uri(_objectId, UriKind.Relative),
                                                                Tags = tags
                                                            });

            var referenceObject = GetObject();

            Assert.IsTrue(referenceObject.Metadata.Contains(tags));
        }

        [TestMethod]
        public void SetUserMetadata_ShouldThrowArgumentException_ForNoTagsProvided()
        {
            ArgumentOutOfRangeException ex = null;
            CreateObject(string.Empty);

            try
            {
                _storageClient.SetUserMetadata(new SetUserMetadataRequest
                                                                {
                                                                    Resource = new Uri(_objectId, UriKind.Relative),
                                                                });
            }
            catch (ArgumentOutOfRangeException exception)
            {
                ex = exception;
            }

            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.Message.Contains("Either tags or listable tags must be supplied to the SetUserMetadata command"));
        }

        [TestMethod]
        public void UpdateObject_ShouldSetObjectContentToEmpty_ForZeroRangeAndEmptyBody()
        {
           CreateObject();

           var response = _storageClient.UpdateObject(new UpdateObjectRequest
           {
               Resource = new Uri(_objectId, UriKind.Relative),
               RangeSpecification = string.Empty,
               Content = new byte[] { }
           });

            var updatedObject = GetObject();

            Assert.IsTrue(response.Delta != 0);
            Assert.AreEqual(0, updatedObject.Content.Length);
        }
    }
}