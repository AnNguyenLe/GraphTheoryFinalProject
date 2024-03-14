using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.GraphTraversal;

namespace GraphTheoryFinalProject.Verifiers;

public interface IGraphChecker
{
    bool IsConnectedGraph(int startVertex = 0);
    bool IsPositiveWeightedGraph();
    bool AnyLoopsOrMultiples();
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
        var dfsResult = DepthFirstSearch.RunDFS(_adjList, startVertex);

        return !dfsResult.AnyNonVisitedVertices;
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

    public bool AnyLoopsOrMultiples()
    {
        var visitedEdges = new Dictionary<string, bool>();
        foreach (var vertex in _adjList.Vertices.Keys)
        {
            foreach (var adjEdge in _adjList.Vertices[vertex])
            {
                // Check self-loop:
                if (vertex == adjEdge.Vertex)
                {
                    return true;
                }

                // Check multiples:
                var edgeName = $"{vertex} - {adjEdge.Vertex}";
                if (visitedEdges.ContainsKey(edgeName))
                {
                    return true;
                }
                visitedEdges.Add(edgeName, true);
            }
        }
        return false;
    }
}



