using System;
using System.Collections.Generic;
using System.Linq;
using Challenge;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChallengeTests
{
    [TestClass]
    public class UserCollectionTests
    {
        private const string NAME = "MPLA_MPLA";
        private const string ELEMENT_DOESNOT_EXIST = "DOES NOT EXIST";
        private readonly List<Tuple<string, string>> NAMES = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("Name1" , "Name2"),
            new Tuple<string, string>("Name3" , "Name4"),
            new Tuple<string, string>("Name5" , "Name6"),
            new Tuple<string, string>("Name7" , "Name8"),
            new Tuple<string, string>("Name9" , "Name10"),
        };
        private List<string> NAME_LIST = new List<string>
        {
            "Name1" , "Name2" , "Name3"
        };
        private List<string> NAME_LIST_WITH_ALREADY_EXISTING_ELEMENTS = new List<string>
        {
            "ANOTHER_NAME1" , "Name2" , "ANOTHER_NAME2", "ANOTHER_NAME3"
        };

        private const int FINAL_NUMBER_OF_UNIQUE_ELEMENTS = 6;
        private List<string> friendList = new List<string>{"FRIEND1" , "FRIEND2" , "FRIEND3"};

        private UserCollection _loader;

        [TestInitialize]
        public void Initialize()
        {
            _loader = new UserCollection();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadShouldNotAllowNullAsInput()
        {
            _loader.Load(((Tuple<string, string>)null));
        }

        [TestMethod]
        public void ShouldNotAddDuplicateEntries()
        {
            _loader.Load(new Tuple<string, string>(NAME, NAME));
            Assert.AreEqual(1 , _loader.Count());

        }

        [TestMethod]
        public void ShouldReturnTheNumberOfEntriesInTheTable()
        {
            foreach (var name in NAMES)
            {
                _loader.Load(name);
            }
            Assert.AreEqual(2 * NAMES.Count, _loader.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldNotAcceptStringsThatDontStartWithAsciiCharacterLessThanCapitalA()
        {
            _loader.Load(new Tuple<string, string>("@" , "@"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ShouldNotAcceptStringsThatDontStartWithAsciiCharacterMoreThanCapitalZ()
        {
            _loader.Load(new Tuple<string, string>("[", "["));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddRangeShuldNotAllowNullAsInput()
        {
            _loader.AddRange(null);
        }

        [TestMethod]
        public void ShouldLoadValuesFromAList()
        {
            _loader.AddRange(NAME_LIST);
            Assert.AreEqual(NAME_LIST.Count , _loader.Count());
        }

        [TestMethod]
        public void ShouldLoadValuesFromAListOnlyIfTheDontExistAlready()
        {
            _loader.AddRange(NAME_LIST);
            _loader.AddRange(NAME_LIST_WITH_ALREADY_EXISTING_ELEMENTS);

            Assert.AreEqual(FINAL_NUMBER_OF_UNIQUE_ELEMENTS, _loader.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesExistWillNotAllowNullValueAsInput()
        {
            _loader.DoesExist(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoesExistWillNotAllowEmptyValueAsInput()
        {
            _loader.DoesExist("   ");
        }

        [TestMethod]
        public void VerifyThatElementInCollectionExists()
        {
            _loader.Load(NAMES[0]);
            Assert.IsTrue(_loader.DoesExist(NAMES[0].Item1));
        }

        [TestMethod]
        public void VerifyThatElementInCollectionDoesNotExists()
        {
            _loader.Load(NAMES[0]);
            Assert.IsFalse(_loader.DoesExist(ELEMENT_DOESNOT_EXIST));
        }

        [TestMethod]
        public void CleanShouldRemoveAllElementsFromCollection()
        {
            _loader.AddRange(NAME_LIST);
            _loader.Clear();

            Assert.AreEqual(0 , _loader.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadShouldNotAllowNullValues()
        {
            _loader.Load((string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadShouldNotAllowWhitespaceValuesAsInput()
        {
            _loader.Load("  ");
        }

        [TestMethod]
        public void ShouldLoadInTheCollectionANewName()
        {
            _loader.Load(NAME);
            Assert.AreEqual(1 , _loader.Count());
        }

        [TestMethod]
        public void WillReturnFalseIfTheNodeYouWantToRemoveDoesNotExist()
        {
            Assert.IsFalse(_loader.Remove(ELEMENT_DOESNOT_EXIST));
        }

        [TestMethod]
        public void WillReturnTrueIfTheNodeYouWantToRemoveExists()
        {
            _loader.Load(NAME);
            Assert.IsTrue(_loader.Remove(NAME));
        }

        [TestMethod]
        public void WillRemoveANodeFromCollection()
        {
            _loader.Load(NAME);
            _loader.Remove(NAME);
            Assert.AreEqual(0 , _loader.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void FindWillThrowIfUserDoesNotExistInTheCollection()
        {
            _loader.Find(ELEMENT_DOESNOT_EXIST);
        }

        [TestMethod]
        public void FindWillReturnUserOfCollection()
        {
            _loader.Load(NAME);
            var user = _loader.Find(NAME);
            Assert.AreEqual(NAME , user.Name);
            Assert.AreEqual(0 , user.Friends.Count);
        }

        [TestMethod]
        public void FriendListWillReturnAListWithName()
        {
            _loader.Load(NAME);
            var user = _loader.Find(NAME);
            foreach (var friendName in friendList)
            {
                user.AddFriend(friendName);
            }

            Assert.IsTrue(friendList.SequenceEqual(_loader.GetFriendList(NAME)));
        }

        [TestMethod]
        public void ShouldBeAbleToWalkthroughAllTheElementsOfTheCollection()
        {
            foreach (var name in NAME_LIST)
            {
                _loader.Load(name);
            }

            var i = 0;
            foreach (var element in _loader)
            {
                Assert.AreEqual(NAME_LIST[i] , ((User)element).Name);
                ++i;
            }
        }
    }
}
