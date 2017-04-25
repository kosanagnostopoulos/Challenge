using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    public class ShortestPath
    {
        private IUserCollection _graph;

        public int FindDistance(IUserCollection graph, string rootNode, string destinationNode)
        {
            VerifyInputAreValid(graph, rootNode, destinationNode);

            return 0;
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
