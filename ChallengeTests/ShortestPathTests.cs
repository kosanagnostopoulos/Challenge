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
        const string ROOT_NAME = "ROOT";
        const string DESTINATION_NAME = "DEST";
        const string ROOT_DOESNOT_EXIST = "ROOTDOESNOTEXIST";
        const string DESTINATION_DOESNOT_EXIST = "DESTINATION_DOESNOT_EXIST";

        private List<User> VISITED_NODES = new List<User>
        {
            new User("VISITED1"),
            new User("NEIGHBOUR1"),
            new User("VISITED2"),
            new User("NEIGHBOUR2"),
            new User("NEIGHBOUR5")
        };

        private List<User> NEIGHBOUR_NODES = new List<User>
        {
            new User("NEIGHBOUR1"),
            new User("NEIGHBOUR2")
        };

        private List<string> FRIENDS_OF_NEIGHBOUR1 = new List<string>
        {
            "NEIGHBOUR2" , "NEIGHBOUR3" , "NEIGHBOUR4"
        };

        private List<string> FRIENDS_OF_NEIGHBOUR2 = new List<string>
        {
            "NEIGHBOUR1" , "NEIGHBOUR4" , "NEIGHBOUR5" , "NEIGHBOUR6"
        };

        private readonly Mock<IUserCollection> _collection = new Mock<IUserCollection>();
        private readonly Mock<IUserCollection> _neighbours = new Mock<IUserCollection>();
        private readonly Mock<IUserCollection> _visited = new Mock<IUserCollection>();
        private ShortestPath _algorithm;

        [TestInitialize]
        public void Initialise()
        {
            _algorithm = new ShortestPath(_neighbours.Object, _visited.Object);
        }

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
            _algorithm.FindDistance(new UserCollection(), ROOT_NAME, ROOT_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RootNodeShouldNotBeNull()
        {
            _algorithm.FindDistance(new UserCollection(), null, DESTINATION_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RootNodeShouldNotNotBeWhiteSpace()
        {
            _algorithm.FindDistance(new UserCollection(), "   ", DESTINATION_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DestinationNodeShouldNotBeNull()
        {
            _algorithm.FindDistance(new UserCollection(), ROOT_NAME, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DestinationNodeShouldNotNotBeWhiteSpace()
        {
            _algorithm.FindDistance(new UserCollection(), ROOT_NAME, "  ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RootNodeShouldExistInGraph()
        {
            _collection.Setup(f => f.DoesExist(It.Is<string>(s => s == ROOT_DOESNOT_EXIST))).Returns(false);
            _collection.Setup(f => f.DoesExist(It.Is<string>(s => s == DESTINATION_NAME))).Returns(true);

            _algorithm.FindDistance(_collection.Object, ROOT_DOESNOT_EXIST, DESTINATION_NAME);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DestinationNodeShouldExistInGraph()
        {
            _collection.Setup(f => f.DoesExist(It.Is<string>(s => s == ROOT_NAME))).Returns(true);
            _collection.Setup(f => f.DoesExist(It.Is<string>(s => s == DESTINATION_DOESNOT_EXIST))).Returns(false);

            _algorithm.FindDistance(_collection.Object, ROOT_NAME, DESTINATION_DOESNOT_EXIST);
        }

        [TestMethod]
        public void NeighbourLayerContainsDestinationNode()
        {
            SetupRootAndDestinationInMockCollection();
            _neighbours.Setup(f => f.DoesExist(It.Is<string>(s => s == DESTINATION_NAME))).Returns(true);

            _algorithm.Initialise(_collection.Object, ROOT_NAME, DESTINATION_NAME);

            Assert.IsTrue(_algorithm.IsDestNodeInNeighbourLayer());
        }

        private void SetupRootAndDestinationInMockCollection()
        {
            _collection.Setup(f => f.DoesExist(It.Is<string>(s => s == ROOT_NAME))).Returns(true);
            _collection.Setup(f => f.DoesExist(It.Is<string>(s => s == DESTINATION_NAME))).Returns(true);
        }

        [TestMethod]
        public void NeighbourLayerDoesNotContainDestinationNode()
        {
            SetupRootAndDestinationInMockCollection();
            _neighbours.Setup(f => f.DoesExist(It.Is<string>(s => s == DESTINATION_NAME))).Returns(false);

            _algorithm.Initialise(_collection.Object, ROOT_NAME, DESTINATION_NAME);

            Assert.IsFalse(_algorithm.IsDestNodeInNeighbourLayer());
        }

        [TestMethod]
        public void NodesInNeghbourLayerThatExistInVisitedLayterWillBeRemoved()
        {
            SetupRootAndDestinationInMockCollection();
            _visited.Setup(f => f.GetEnumerator()).Returns(VISITED_NODES.GetEnumerator());

            _algorithm.RemoveNeighbourNodesThatExistInVisitedNodes();

            foreach (var visitedNode in VISITED_NODES)
            {
            _neighbours.Verify(f => f.Remove(It.Is<string>(s => s == visitedNode.Name)) , Times.Exactly(1));
            }
            _neighbours.Verify(f => f.Remove(It.IsAny<string>()), Times.Exactly(VISITED_NODES.Count));
        }

        [TestMethod]
        public void WillLoadAllFriendsForAllMembersOfNeighbourLayer()
        {
            SetupRootAndDestinationInMockCollection();
            _neighbours.Setup(f => f.GetEnumerator()).Returns(NEIGHBOUR_NODES.GetEnumerator());
            _neighbours.Setup(f => f.GetFriendList(It.Is<string>(s => s == NEIGHBOUR_NODES[0].Name)))
                .Returns(FRIENDS_OF_NEIGHBOUR1);
            _neighbours.Setup(f => f.GetFriendList(It.Is<string>(s => s == NEIGHBOUR_NODES[1].Name)))
                .Returns(FRIENDS_OF_NEIGHBOUR2);

            _algorithm.LoadNewNeighbourLayer();
        }
    }
}
