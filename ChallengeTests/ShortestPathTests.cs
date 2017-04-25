using System;
using System.Collections.Generic;
using Challenge;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChallengeTests
{
    [TestClass]
    public class ShortestPathTests
    {
        private readonly IUserCollection DONT_CARE = null;
        const string ROOT_NAME = "root";
        const string DESTINATION_NAME = "dest";
        const string ROOT_DOESNOT_EXIST = "RootDoesNotExist";
        const string DESTINATION_DOESNOT_EXIST = "RootDoesNotExist";

        private readonly ShortestPath _algorithm = new ShortestPath();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GraphShouldNotBeNull()
        {
            _algorithm.FindDistance(null, ROOT_NAME, DESTINATION_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RootNodeAndDestinationNodeShouldBeDifferent()
        {
            _algorithm.FindDistance(new UserCollection(new List<string>()), ROOT_NAME, ROOT_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RootNodeShouldNotBeNull()
        {
            _algorithm.FindDistance(new UserCollection(new List<string>()), null, DESTINATION_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RootNodeShouldNotNotBeWhiteSpace()
        {
            _algorithm.FindDistance(new UserCollection(new List<string>()), "   ", DESTINATION_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DestinationNodeShouldNotBeNull()
        {
            _algorithm.FindDistance(new UserCollection(new List<string>()), ROOT_NAME, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DestinationNodeShouldNotNotBeWhiteSpace()
        {
            _algorithm.FindDistance(new UserCollection(new List<string>()), ROOT_NAME, "  ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RootNodeShouldExistInGraph()
        {
            Mock<IUserCollection> collection = new Mock<IUserCollection>();
            collection.Setup(f => f.DoesExist(It.Is<string>(s => s == ROOT_DOESNOT_EXIST))).Returns(false);
            collection.Setup(f => f.DoesExist(It.Is<string>(s => s == DESTINATION_NAME))).Returns(true);

            _algorithm.FindDistance(collection.Object, ROOT_DOESNOT_EXIST, DESTINATION_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DestinationNodeShouldExistInGraph()
        {
            Mock<IUserCollection> collection = new Mock<IUserCollection>();
            collection.Setup(f => f.DoesExist(It.Is<string>(s => s == ROOT_NAME))).Returns(true);
            collection.Setup(f => f.DoesExist(It.Is<string>(s => s == DESTINATION_DOESNOT_EXIST))).Returns(false);

            _algorithm.FindDistance(collection.Object, ROOT_NAME, DESTINATION_DOESNOT_EXIST);
        }
    }
}
