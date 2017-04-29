﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChallengeTests
{
    [TestClass]
    public class UserCollectionEnumeratorTests: UserCollectionEnumerator
    {
        private const int FIRST_ELEMENT = 0;
        private const int SECOND_ARRAY = 1;

        public UserCollectionEnumeratorTests() : base(
            new List<string>[]
            {
                new List<string>{"ELEM1" , "ELEM2"} ,
                new List<string>{"ELEM3"},
            }) { }

        [TestMethod]
        public void ResetShouldReinitializeArrayAndElementPointers()
        {
            MoveNext();
            MoveNext();
            MoveNext();

            Reset();

            Assert.AreEqual(ARRAY_RESET_POSITION, _arrayPointer);
            Assert.AreEqual(ELEMENT_RESET_POSITION, _elementPointer);
        }

        [TestMethod]
        public void ShouldMoveElementPointerToNextItemOfCurrentArrayIfThereAreavailableItems()
        {
            Reset();

            MoveNext();

            Assert.AreEqual(FIRST_ELEMENT , _elementPointer);
        }

        [TestMethod]
        public void ShouldMoveToFirstElementOfNextArrayIfCurrentArrayDoesnNotHaveAnyOtherElements()
        {
            Reset();

            MoveNext();
            MoveNext();
            MoveNext();

            Assert.AreEqual(ELEMENT_FIRST_POSITION , _elementPointer);
            Assert.AreEqual(SECOND_ARRAY , _arrayPointer);
        }

        [TestMethod]
        public void ShouldReturnTrueIfThereAreELementsInTheCollection()
        {
            Reset();
            Assert.IsTrue(MoveNext());
        }

        [TestMethod]
        public void ShouldReturnFalseIfThereAreNOotELementsInTheCollection()
        {
            Reset();
            MoveNext();
            MoveNext();
            MoveNext();

            Assert.IsFalse(MoveNext());
        }

        [TestMethod]
        public void EnumeratorWillReturnAllElementsFromCollection()
        {
            Reset();

            MoveNext();
            Assert.AreEqual(_index[0][0] , Current);
            MoveNext();
            Assert.AreEqual(_index[0][1], Current);
            MoveNext();
            Assert.AreEqual(_index[1][0], Current);

        }

    }
}