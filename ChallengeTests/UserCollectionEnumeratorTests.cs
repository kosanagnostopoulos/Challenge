using System;
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
        private const int THIRD_ARRAY = 2;

        public UserCollectionEnumeratorTests() : base(
            new List<User>[]
            {
                new List<User>(),
                new List<User>(), 
                new List<User>{new User("ELEM1") , new User("ELEM2")} ,
                new List<User>{new User("ELEM3")},
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

            Assert.AreEqual(ELEMENT_FIRST_POSITION , _elementPointer);
            Assert.AreEqual(THIRD_ARRAY, _arrayPointer);
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
            Assert.AreEqual(_index[2][0] , Current);
            MoveNext();
            Assert.AreEqual(_index[2][1], Current);
            MoveNext();
            Assert.AreEqual(_index[3][0], Current);

        }

    }
}
