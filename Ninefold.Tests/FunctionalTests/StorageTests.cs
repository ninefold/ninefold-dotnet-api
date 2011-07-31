﻿using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninefold.API.Storage.Messages;

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

        private void CreateObject()
        {
            var response = _storageClient.StoredObject.CreateObject(new CreateObjectRequest
            {
                Content = _demoContent,
                Resource = new Uri("objects", UriKind.Relative),
                GroupACL = "other=NONE",
                ACL = "godbold=FULL_CONTROL",
                Metadata = "part1=buy",
                ListableMetadata = "part4/part7/part8=quick"
            });

            _objectId = response.Location;
        }

        private void DeleteLastObject()
        {
            _storageClient.StoredObject.DeleteObject(new DeleteObjectRequest
            {
                Resource = new Uri(_objectId, UriKind.Relative)
            });
        }

        private void CreateNamespace(string path)
        {
            var response = _storageClient.StoredObject.CreateObject(new CreateObjectRequest
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
            var response = _storageClient.StoredObject.CreateObject(new CreateObjectRequest
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
            var response = _storageClient.StoredObject.CreateObject(new CreateObjectRequest
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
            var response = _storageClient.StoredObject.CreateObject(new CreateObjectRequest
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
            var response = _storageClient.StoredObject.GetObject(new GetObjectRequest
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
                _storageClient.StoredObject.GetObject(new GetObjectRequest
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
                _storageClient.StoredObject.GetObject(new GetObjectRequest
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
                _storageClient.StoredObject.ListNamespace(new ListNamespaceRequest
                {
                    Resource = new Uri(_objectId, UriKind.Relative),
                });
            var actualContent = Encoding.ASCII.GetString(response.Content);

            Assert.IsFalse(string.IsNullOrWhiteSpace(actualContent));
        }

        [TestMethod]
        public void GetObject_ShouldReturnNamespaceListing_ForGetNamespaceWithSpaces()
        {
            CreateNamespace("namespace/test namespace/");
            var response =
                _storageClient.StoredObject.ListNamespace(new ListNamespaceRequest
                {
                    Resource = new Uri(_objectId, UriKind.Relative),
                });
            var actualContent = Encoding.ASCII.GetString(response.Content);

            Assert.IsFalse(string.IsNullOrWhiteSpace(actualContent));
        }

        [TestMethod]
        public void DeleteObject_ShouldDeleteNamespace_ForValidPath()
        {
            CreateNamespace("namespace/left/right/");
            var response = _storageClient.StoredObject.DeleteObject(new DeleteObjectRequest
            {
                Resource = new Uri(_objectId, UriKind.Relative)
            });
            _objectId = string.Empty;

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void UpdateObject_ShouldUpdateExistingObject_ToNewContent()
        {
            CreateObject();

            var updateContent = Encoding.ASCII.GetBytes("Updated text for a file");
            var response = _storageClient.StoredObject.UpdateObject(new UpdateObjectRequest
            {
                Resource = new Uri(_objectId, UriKind.Relative),
                Content = updateContent
            });

            Assert.IsTrue(response.Delta != 0);
        }

        [TestMethod]
        public void DeleteObject_ShouldDeleteObject_ForValidRequest()
        {
            CreateObject();

            var response = _storageClient.StoredObject.DeleteObject(new DeleteObjectRequest
                                                                        {
                                                                            Resource =
                                                                                new Uri(_objectId, UriKind.Relative)
                                                                        });
            _objectId = string.Empty;
            Assert.IsTrue(response.Delta != 0);
        }

        [TestMethod]
        public void SetACL_ShouldSetACLOnExistingObject_ForValidRequest()
        {
            CreateObject();

            _storageClient.StoredObject.SetObjectACL(new SetObjectACLRequest
                {
                    Resource = new Uri(_objectId, UriKind.Relative),
                    UserACL = "godbold=FULL_CONTROL, somone=READ"
                });
        }
    }
}