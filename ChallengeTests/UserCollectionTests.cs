﻿using System;
using System.Collections.Generic;
using Challenge;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChallengeTests
{
    [TestClass]
    public class UserCollectionTests
    {
        private const string NAME = "MPLA_MPLA";
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
    }
}
