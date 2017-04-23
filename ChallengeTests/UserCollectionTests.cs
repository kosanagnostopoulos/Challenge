using System;
using System.Collections.Generic;
using Challenge;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChallengeTests
{
    [TestClass]
    public class UserCollectionTests
    {
        private const string NAME = "MPLA_MPLA";
        private List<Tuple<string, string>> _names = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("Name1" , "Name2"),
            new Tuple<string, string>("Name3" , "Name4"),
            new Tuple<string, string>("Name5" , "Name6"),
            new Tuple<string, string>("Name7" , "Name8"),
            new Tuple<string, string>("Name9" , "Name10"),
        };
        private UserCollection _loader;

        [TestInitialize]
        public void Initialize()
        {
            _loader = new UserCollection(new List<string>());
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
            foreach (var name in _names)
            {
                _loader.Load(name);
            }
            Assert.AreEqual(2 * _names.Count, _loader.Count());
        }
    }
}
