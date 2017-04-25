using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    public class ShortestPath
    {
        private const int NOT_FOUND = -1;

        private IUserCollection _graph;
        private bool _destNodeFound = false;
        private int _distance = 0;
        private IUserCollection _neighbourLayer;
        private IUserCollection _visitedNodes;

        public int FindDistance(IUserCollection graph, string rootNode, string destinationNode)
        {
            VerifyInputAreValid(graph, rootNode, destinationNode);
            InitialiseCollections();

            Initialise(rootNode);
            do
            {
                ++_distance;
                LoadNewNeighbourLayer();
                _destNodeFound = IsDestNodeInNeighbourLayer();
                RemoveNeighbourNodesThatExistInVisitedNodes();
            } while (_destNodeFound && _neighbourLayer.Count() != 0);

            return _destNodeFound ? _distance : NOT_FOUND;
        }

        private void Initialise(string rootNode)
        {
            _distance = 0;
            _destNodeFound = false;
            _neighbourLayer.Load(rootNode);
            _visitedNodes.Load(rootNode);
        }

        private void InitialiseCollections()
        {
            _neighbourLayer.Clear();
            _visitedNodes.Clear();
        }

        public bool IsDestNodeInNeighbourLayer()
        {
            throw new NotImplementedException();
        }

        public void RemoveNeighbourNodesThatExistInVisitedNodes()
        {
            throw new NotImplementedException();
        }

        public void LoadNewNeighbourLayer()
        {
            throw new NotImplementedException();
        }

        private void VerifyInputAreValid(IUserCollection graph, string rootNode, string destinationNode)
        {
            VerifyInputHaveContent(graph, rootNode, destinationNode);
            VeifyRootAndDestinationAreDifferent(rootNode, destinationNode);

            _graph = graph;
            VerifyNodesExist(rootNode, destinationNode);
        }

        private void VerifyNodesExist(string rootNode, string destinationNode)
        {
            if (!_graph.DoesExist(rootNode))
            {
                throw new ArgumentOutOfRangeException(nameof(rootNode));
            }

            if (!_graph.DoesExist(destinationNode))
            {
                throw new ArgumentOutOfRangeException(nameof(destinationNode));
            }
        }

        private static void VerifyInputHaveContent(IUserCollection graph, string rootNode, string destinationNode)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            if (string.IsNullOrWhiteSpace(rootNode))
            {
                throw new ArgumentException(nameof(rootNode));
            }

            if (string.IsNullOrWhiteSpace(destinationNode))
            {
                throw new ArgumentException(nameof(destinationNode));
            }
        }

        private static void VeifyRootAndDestinationAreDifferent(string rootNode, string destinationNode)
        {
            if (string.Equals(rootNode, destinationNode))
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
