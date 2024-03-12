using GraphTheoryFinalProject.Entities;

namespace GraphTheoryFinalProject.SpecialGraphs;

public class Barbell
{
    private readonly AdjacencyList _adjList;
    public Barbell(AdjacencyList adjList)
    {
        _adjList = adjList;
    }

    public int CountDegree()
    {
        var INVALID_VALUE = -1;
        var barEdge = GetBarEdge();
        if (barEdge == null)
        {
            return INVALID_VALUE;
        }

        var barEndpoints = new int[2] { barEdge.StartVertex, barEdge.EndVertex };

        Dictionary<int, int> groupOfVertices = new();

        var degrees = new int[2];
        for (int i = 0; i < barEndpoints.Length; i++)
        {
            var endpoint = barEndpoints[i];
            var otherEndpoint = i == 0 ? barEndpoints[1] : barEndpoints[0];

            var degreeCounter = 1;
            foreach (var adjEdge in _adjList.Vertices[endpoint])
            {
                var vertex = adjEdge.Vertex;

                if (groupOfVertices.ContainsKey(vertex))
                {
                    return INVALID_VALUE;
                }
                if (vertex != otherEndpoint)
                {
                    groupOfVertices.Add(vertex, endpoint);
                    ++degreeCounter;
                }
            }
            degrees[i] = degreeCounter;
        }

        // Check if k-Completes at both ends have the same degree
        if (degrees[0] != degrees[1])
        {
            return INVALID_VALUE;
        }
        var degree = degrees[0];

        var totalEdges = _adjList.CountTotalEdges();
        var validNoOfEdges = degree * (degree - 1) + 1;
        if (totalEdges != validNoOfEdges)
        {
            return INVALID_VALUE;
        }

        var validNoOfVertices = 2 * degree;
        if (_adjList.NoOfVertices != validNoOfVertices)
        {
            return INVALID_VALUE;
        }

        return degree;
    }

    public void Display()
    {
        var degree = CountDegree();
        var degreeText = degree == -1 ? "None." : degree.ToString();
        Console.WriteLine($"Barbell graph: {degreeText}");
    }

    private Edge? GetBarEdge()
    {
        var data = _adjList.Vertices;
        var highestDegree = data.Keys.Max(vertex => _adjList.Vertices[vertex].Count);
        var endpoints = data.Keys.Where(vertex => data[vertex].Count == highestDegree).ToArray();

        if (endpoints.Length != 2)
        {
            return null;
        }

        var adjEdge = data[endpoints[0]].FirstOrDefault(adjEdge => adjEdge.Vertex == endpoints[1]);

        if (adjEdge == null)
        {
            return null;
        }

        return new Edge(endpoints[0], endpoints[1], adjEdge.Weight);
    }
}

