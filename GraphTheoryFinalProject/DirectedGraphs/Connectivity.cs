using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.Enums;
using GraphTheoryFinalProject.GraphTraversal;

namespace GraphTheoryFinalProject.DirectedGraphs;

class ReachabilityResult
{
    public bool[,] Reachability { get; init; }
    public bool IsFullOfOnes { get; init; }

    public ReachabilityResult(bool[,] reachability, bool isFullOfOnes)
    {
        Reachability = reachability;
        IsFullOfOnes = isFullOfOnes;
    }
}

public class Connectivity
{
    private readonly AdjacencyMatrix _adjMatrix;

    public Connectivity(AdjacencyMatrix adjMatrix)
    {
        _adjMatrix = adjMatrix;
    }

    public ConnectivityStatus CheckConnectivityStatus()
    {
        int totalVertices = _adjMatrix.NoOfVertices;
        // Table to store if there exist a path from vertex A to vertex Batrix (for every pair of vertices)
        var reachabilityTable = TabulateReachabilityBetweenPairsInDirectedGraph(_adjMatrix);

        if (reachabilityTable.IsFullOfOnes)
        {
            return ConnectivityStatus.StronglyConnected;
        }

        // Reachability table after opposite direction for all edges
        var transposedMatrix = AdjacencyMatrix.Transpose(_adjMatrix);
        var opReachabilityTable = TabulateReachabilityBetweenPairsInDirectedGraph(transposedMatrix);

        var r1 = reachabilityTable.Reachability;
        var r2 = opReachabilityTable.Reachability;

        var isUnilaterallyConnected = true;
        var fromVertex = 0;
        while (isUnilaterallyConnected && fromVertex < totalVertices)
        {
            for (int toVertex = 0; toVertex < totalVertices; toVertex++)
            {
                if (fromVertex == toVertex)
                {
                    continue;
                }
                var isReachableInSimpleGraph = r1[fromVertex, toVertex] || r1[toVertex, fromVertex] || r2[fromVertex, toVertex] || r2[toVertex, fromVertex];
                if (!isReachableInSimpleGraph)
                {
                    isUnilaterallyConnected = false;
                    break;
                }
            }
            ++fromVertex;
        }

        if (isUnilaterallyConnected)
        {
            return ConnectivityStatus.UnilaterallyConnected;
        }

        if (!IsReachableForAllPairInNonDirectedGraph(_adjMatrix, transposedMatrix))
        {
            return ConnectivityStatus.Disconnected;
        }

        return ConnectivityStatus.WeaklyConnected;
    }

    private static ReachabilityResult TabulateReachabilityBetweenPairsInDirectedGraph(AdjacencyMatrix adjMatrix)
    {
        var isFullOfOnes = true;
        var totalVertices = adjMatrix.NoOfVertices;
        var reachabilityTable = new bool[totalVertices, totalVertices];
        for (int fromVertex = 0; fromVertex < totalVertices; fromVertex++)
        {
            var dfsResult = DepthFirstSearch.RunDFS(adjMatrix, fromVertex);
            for (var toVertex = 0; toVertex < totalVertices; toVertex++)
            {
                var visitedVertices = dfsResult.VisitedVertices;
                if (!visitedVertices[toVertex])
                {
                    isFullOfOnes = false;
                }
                if (fromVertex == toVertex || visitedVertices[toVertex])
                {
                    reachabilityTable[fromVertex, toVertex] = true;
                }
            }
        }

        return new ReachabilityResult(reachabilityTable, isFullOfOnes);
    }

    private static bool IsReachableForAllPairInNonDirectedGraph(AdjacencyMatrix matrix, AdjacencyMatrix transposedMatrix)
    {
        var totalVertices = matrix.NoOfVertices;

        var data = matrix.Data;
        var transposedData = transposedMatrix.Data;

        var adjMatrix = new decimal[totalVertices, totalVertices];

        for (int i = 0; i < totalVertices; i++)
        {
            for (int j = 0; j < totalVertices; j++)
            {
                var value = data[i, j] + data[j, i] + transposedData[i, j] + transposedData[j, i];
                adjMatrix[i, j] = value;
                adjMatrix[j, i] = value;
            }
        }

        return !DepthFirstSearch.RunDFS(new AdjacencyMatrix(totalVertices, adjMatrix)).AnyNonVisitedVertices;
    }
}

