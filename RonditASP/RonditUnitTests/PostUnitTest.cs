using Data_layer.DALs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RonditASP.Models;
using System;
using Xunit.Sdk;

namespace RonditUnitTests
{
    [TestClass]
    public class PostUnitTest
    {
        User admin = new User(1, "ronliebregts", "ronliebregts@hotmail.nl", "admin");

        [TestMethod]
        public void TestMethod()
        {
            int result = 5 + 5;

            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void TestPostTitleLengthShort()
        {
            Post post = new Post(1, admin, "kort titel", "korte inhoud", DateTime.Now, 50);

            PostContainer postContainer = new PostContainer(new PostDAL());

            Assert.IsTrue(postContainer.ValidatePostTitle(post));
        }

        [TestMethod]
        public void TestPostTitleLong()
        {
            Post post = new Post(1, admin, "kort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titel", "korte inhoud", DateTime.Now, 50);

            PostContainer postContainer = new PostContainer(new PostDAL());

            Assert.IsFalse(postContainer.ValidatePostTitle(post));
        }

        [TestMethod]
        public void TestPostDescriptionShort()
        {
            Post post = new Post(1, admin, "korte titel", "korte inhoud", DateTime.Now, 50);

            PostContainer postContainer = new PostContainer(new PostDAL());

            Assert.IsTrue(postContainer.ValidatePostDescription(post));
        }

        [TestMethod]
        public void TestPostDescriptionLong()
        {
            Post post = new Post(1, admin, "korte titel", "kort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titel", DateTime.Now, 50);

            PostContainer postContainer = new PostContainer(new PostDAL());

            Assert.IsFalse(postContainer.ValidatePostDescription(post));
        }

        [TestMethod]
        public void TestPostBothShort()
        {
            Post post = new Post(1, admin, "korte titel", "korte inhoud" , DateTime.Now, 50);

            PostContainer postContainer = new PostContainer(new PostDAL());

            Assert.IsTrue(postContainer.ValidatePostTitle(post) && postContainer.ValidatePostDescription(post));
        }

        [TestMethod]
        public void TestPostBothLong()
        {
            Post post = new Post(1, admin, "kort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titel", "kort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort " +
                "titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titelkort titel", DateTime.Now, 50);

            PostContainer postContainer = new PostContainer(new PostDAL());

            Assert.IsFalse(postContainer.ValidatePostTitle(post) && postContainer.ValidatePostDescription(post));
        }
    }
}
