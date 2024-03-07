using GraphTheoryFinalProject.Entities;
using System.Collections;

namespace GraphTheoryFinalProject.GraphActions;

public interface IGraphChecker
{
    bool IsConnectedGraph(int startVertex = 0);
    bool IsPositiveWeightedGraph();

}

public class GraphChecker : IGraphChecker
{
    private readonly AdjacencyList _adjList;

    public GraphChecker(AdjacencyList adjList)
    {
        _adjList = adjList;
    }

    public bool IsConnectedGraph(int startVertex = 0)
    {
        var visitedVertices = new BitArray(_adjList.NoOfVertices);
        var orderOfVisitedVertices = new List<int>();

        (new GraphTraversal(_adjList)).DFS(startVertex, visitedVertices, orderOfVisitedVertices);

        return visitedVertices.Cast<bool>().All(value => value == true);
    }

    public bool IsPositiveWeightedGraph()
    {
        foreach (var vertex in _adjList.Vertices.Keys)
        {
            var adjEdges = _adjList.Vertices[vertex];
            foreach (var adjEdge in adjEdges)
            {
                if (adjEdge.Weight < 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
}

