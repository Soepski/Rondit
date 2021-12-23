using Data_layer.DALs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RonditASP.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RonditUnitTests
{
    [TestClass]
    public class UserUnitTest
    {
        UserContainer userContainer = new UserContainer(new UserDAL());
        User admin = new User(1, "ronliebregts", "ronliebregts@hotmail.nl", "admin");
        User user = new User(3, "pieterpost", "fouteemailathotmail.nl", "user");

        [TestMethod]
        public void ValidateEmailWith()
        {
            Assert.IsTrue(admin.ValidateEmail());
        }

        [TestMethod]
        public void ValidateEmailWithout()
        {
            Assert.IsFalse(user.ValidateEmail());
        }

        [TestMethod]
        public void ValidateUsernameLengthShort()
        {
            Assert.IsTrue(userContainer.ValidateUsernameLength("This is a short username"));
        }

        [TestMethod]
        public void ValidateUsernameLengthLong()
        {
            Assert.IsFalse(userContainer.ValidateUsernameLength("This is a long username that is longer than 50 characters and should not be allowed"));
        }

        [TestMethod]
        public void ValidateEmailLengthShort()
        {
            Assert.IsTrue(userContainer.ValidateUsernameLength("short@email.com"));
        }

        [TestMethod]
        public void ValidateEmailLengthLong()
        {
            Assert.IsFalse(userContainer.ValidateUsernameLength("thisisaverylongemailaddresswhichshouldnotbeallowed@notallowed.com"));
        }

        [TestMethod]
        public void ValidatePasswordLengthShort()
        {
            Assert.IsTrue(userContainer.ValidateUsernameLength("thisisashortpassword"));
        }

        [TestMethod]
        public void ValidatePasswordLengthLong()
        {
            Assert.IsFalse(userContainer.ValidateUsernameLength("itisweirdthatyourpasswordhasalimitbutthisisjustfortestingpurposes"));
        }
    }
}
