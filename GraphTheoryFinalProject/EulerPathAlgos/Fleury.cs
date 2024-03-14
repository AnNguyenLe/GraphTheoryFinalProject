using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.Verifiers;

namespace GraphTheoryFinalProject.EulerPathAlgos;

public class Fleury
{
    private readonly AdjacencyList _adjList;
    private readonly List<int> _oddDegreeVertices;
    public Fleury(AdjacencyList adjList, List<int> oddDegreeVertices)
    {
        _adjList = adjList;
        _oddDegreeVertices = oddDegreeVertices;
    }

    public List<int> Generate()
    {
        var path = new List<int>();
        if (_oddDegreeVertices.Count > 2)
        {
            return path;
        }

        int startVertex = _oddDegreeVertices.Count == 2 ? _oddDegreeVertices[0] : 0;
        int departure = startVertex;
        path.Add(startVertex);

        var graph = AdjacencyList.Clone(_adjList);

        var graphData = graph.Vertices;

        // Based on handshaking theorem:
        var noOfRemainingEdges = graph.CountTotalEdges();

        var visitedEdges = new Dictionary<int, bool>();
        var graphWithRemovedEdges = graph;

        var threshold = _oddDegreeVertices.Count == 0 ? 1 : 0;

        while (noOfRemainingEdges > threshold)
        {
            var nextEdge = SelectNextEdge(ref graphWithRemovedEdges, visitedEdges, startVertex);
            visitedEdges.Add(nextEdge.GetHashCode(), true);

            startVertex = nextEdge.EndVertex;
            path.Add(startVertex);
            --noOfRemainingEdges;
        }
        if (_oddDegreeVertices.Count == 0)
        {
            path.Add(departure);
        }
        return path;
    }

    private static Edge SelectNextEdge(ref AdjacencyList adjList, Dictionary<int, bool> visitedEdges, int startVertex)
    {
        var adjEdges = adjList.Vertices[startVertex];
        Edge bridge = null;

        foreach (var adjEdge in adjEdges)
        {
            var cadidateEdge = new Edge(startVertex, adjEdge.Vertex, adjEdge.Weight);

            var hasBeenVisitedEdge = visitedEdges.ContainsKey(cadidateEdge.GetHashCode());
            if (hasBeenVisitedEdge)
            {
                continue;
            }

            var graphWithoutCadidateEdge = CreateGraphWithRemovedEdge(adjList, cadidateEdge);
            var isBridge = !(new GraphChecker(graphWithoutCadidateEdge)).IsConnectedGraph();

            // Only use bridge whenever it is the only choice
            if (isBridge)
            {
                bridge = cadidateEdge;
            }
            else
            {
                adjList = graphWithoutCadidateEdge;
                return cadidateEdge;
            }
        }

        return bridge!;

    }

    public void Display(List<int> path)
    {
        if (path.Count == 0)
        {
            Console.WriteLine("This is a Non-Eulerian Graph.");
            return;
        }
        if (_oddDegreeVertices.Count == 0)
        {
            Console.WriteLine("This is an Eulerian Graph.");
            Console.WriteLine($"Euler Circuit: {string.Join(" --> ", path)}");
        }
        else
        {
            Console.WriteLine("This is a Semi-Eulerian Graph.");
            Console.WriteLine($"Euler Path: {string.Join(" --> ", path)}");
        }
    }

    private static AdjacencyList CreateGraphWithRemovedEdge(AdjacencyList adjacencyList, Edge removedEdge)
    {
        var graphData = new Dictionary<int, List<AdjacentEdge>>();

        foreach (var vertex in adjacencyList.Vertices.Keys)
        {
            if (vertex == removedEdge.StartVertex || vertex == removedEdge.EndVertex)
            {
                graphData[vertex] = adjacencyList.Vertices[vertex]
                    .Where(adjEdge => adjEdge.Vertex != removedEdge.StartVertex && adjEdge.Vertex != removedEdge.EndVertex)
                    .ToList();
            }
            else
            {
                graphData[vertex] = adjacencyList.Vertices[vertex];
            }
        }

        return new AdjacencyList(adjacencyList.NoOfVertices, graphData);
    }

}

