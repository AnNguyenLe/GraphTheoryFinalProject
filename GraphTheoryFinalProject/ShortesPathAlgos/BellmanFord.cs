using GraphTheoryFinalProject.Entities;

namespace GraphTheoryFinalProject.ShortesPathAlgos;

public class BellmanFord
{
    private readonly int _startVertex;
    private readonly int _endVertex;
    private readonly AdjacencyList _adjList;

    public BellmanFord(AdjacencyList adjList, int startVertex, int endVertex)
    {
        _adjList = adjList;
        _startVertex = startVertex;
        _endVertex = endVertex;
    }

    public Tuple<Dictionary<int, decimal>, Dictionary<int, int>> Generate()
    {
        Dictionary<int, decimal> distances = new()
        {
            { _startVertex, 0 }
        };

        Dictionary<int, int> prevNodes = new()
        {
            { _startVertex, _startVertex }
        };

        if (_startVertex == _endVertex)
        {
            return Tuple.Create(distances, prevNodes);
        }

        // Init tracking table
        for (var i = 0; i < _adjList.NoOfVertices; i++)
        {
            if (_startVertex != i)
            {
                distances.Add(i, decimal.MaxValue);
            }
        }

        var repetitionTimes = _adjList.NoOfVertices - 1;
        var edges = GetAllEdges();

        for (var i = 0; i < repetitionTimes; i++)
        {
            foreach (var edge in edges)
            {
                if (distances[edge.StartVertex] == decimal.MaxValue)
                {
                    continue;
                }
                var newDistance = distances[edge.StartVertex] + edge.Weight;
                if (newDistance < distances[edge.EndVertex])
                {
                    distances[edge.EndVertex] = newDistance;
                    prevNodes[edge.EndVertex] = edge.StartVertex;
                }
            }
        }

        return Tuple.Create(distances, prevNodes);
    }

    public void Display(Tuple<Dictionary<int, decimal>, Dictionary<int, int>> distancesAndPrevNodes)
    {
        List<int> trace = new() { _endVertex };

        var distances = distancesAndPrevNodes.Item1;
        var prevNodes = distancesAndPrevNodes.Item2;

        int prevNode = _endVertex;
        while (prevNode != _startVertex)
        {
            prevNode = prevNodes[prevNode];
            trace.Add(prevNode);
        }

        trace.Reverse();

        Console.WriteLine($"{string.Join(" --> ", trace)}: {distances[_endVertex]}");
    }

    private List<Edge> GetAllEdges()
    {
        var vertices = _adjList.Vertices.Keys.ToList();
        List<Edge> edges = new();

        foreach (var vertex in vertices)
        {
            foreach (var adjEdge in _adjList.Vertices[vertex])
            {
                edges.Add(new Edge(vertex, adjEdge.Vertex, adjEdge.Weight));
            }
        }

        return edges;
    }
}

