using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Challenge;

namespace ChallengeTests
{
    [TestClass]
    public class UserTests
    {
        private const string USERNAME = "Name1";
        private const string NON_EXISTING_FRIEND = "Name2";
        private const string EXISTING_FRIEND = "Name2";

        private User _user;

        [TestInitialize]
        public void Initialise()
        {
            _user = new User(USERNAME);
        }

        [TestMethod]
        public void WillAddANameInFriendListIfTheNameDoesNotExist()
        {
            _user.AddFriend(NON_EXISTING_FRIEND);
            Assert.AreEqual(1 , _user.Friends.Count);
        }

        [TestMethod]
        public void WillNotAddANameInFriendListIfTheNameExist()
        {
            _user.AddFriend(NON_EXISTING_FRIEND);
            _user.AddFriend(EXISTING_FRIEND);
            Assert.AreEqual(1, _user.Friends.Count);
        }
    }
}
