namespace Challenge
{
    public interface IShortestPath
    {
        int FindDistance(IUserCollection graph, string rootNode, string destinationNode);
    }
}