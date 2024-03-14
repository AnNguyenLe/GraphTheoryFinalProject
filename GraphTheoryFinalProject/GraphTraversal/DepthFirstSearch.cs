
using GraphTheoryFinalProject.Entities;
using System.Collections;

namespace GraphTheoryFinalProject.GraphTraversal;

public class DFSResult
{
    public BitArray VisitedVertices { get; init; }
    public List<int> OrderOfVisitedVertices { get; init; }
    public bool AnyNonVisitedVertices { get; init; }

    public DFSResult(BitArray visitedVertices, List<int> orderOfVisitedVertices)
    {
        VisitedVertices = visitedVertices;
        OrderOfVisitedVertices = orderOfVisitedVertices;
        AnyNonVisitedVertices = VisitedVertices.Cast<bool>().Any(visited => visited == false);
    }
}

public interface IDepthFirstSearch
{
    static abstract DFSResult RunDFS(AdjacencyList adjList, int startVertex = 0);
    static abstract DFSResult RunDFS(AdjacencyMatrix adjMatrix, int startVertex = 0);
}

public class DepthFirstSearch : IDepthFirstSearch
{

    public static DFSResult RunDFS(AdjacencyList adjList, int startVertex = 0)
    {
        var visitedVertices = new BitArray(adjList.NoOfVertices);
        var orderOfVisitedVertices = new List<int>();

        DFSOnAdjacencyList(adjList, startVertex, visitedVertices, orderOfVisitedVertices);
        return new DFSResult(visitedVertices, orderOfVisitedVertices);
    }

    public static DFSResult RunDFS(AdjacencyMatrix adjMatrix, int startVertex = 0)
    {
        var visitedVertices = new BitArray(adjMatrix.NoOfVertices);
        var orderOfVisitedVertices = new List<int>();

        DFSOnAdjacencyMatrix(adjMatrix, startVertex, visitedVertices, orderOfVisitedVertices);
        return new DFSResult(visitedVertices, orderOfVisitedVertices);
    }

    private static void DFSOnAdjacencyList(AdjacencyList adjList, int startVertex, BitArray visited, List<int> orderOfVisited)
    {
        if (visited[startVertex])
        {
            return;
        }
        foreach (var edge in adjList.Vertices[startVertex])
        {

            if (!visited[startVertex])
            {
                orderOfVisited.Add(startVertex);
                visited.Set(startVertex, true);
            }

            DFSOnAdjacencyList(adjList, edge.Vertex, visited, orderOfVisited);
        }
    }

    private static void DFSOnAdjacencyMatrix(AdjacencyMatrix adjMatrix, int startVertex, BitArray visited, List<int> orderOfVisited)
    {
        var data = adjMatrix.Data;

        if (visited[startVertex])
        {
            return;
        }

        for (int i = 0; i < adjMatrix.NoOfVertices; i++)
        {
            if (data[startVertex, i] == 0)
            {
                continue;
            }

            if (!visited[startVertex])
            {
                orderOfVisited.Add(startVertex);
                visited.Set(startVertex, true);
            }

            DFSOnAdjacencyMatrix(adjMatrix, i, visited, orderOfVisited);
        }
    }
}





