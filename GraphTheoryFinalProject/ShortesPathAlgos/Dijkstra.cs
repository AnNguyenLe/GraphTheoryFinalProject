using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using GraphTheoryFinalProject.Entities;

namespace GraphTheoryFinalProject.ShortesPathAlgos;

public class Dijkstra
{
    private readonly int _startVertex;
    private readonly int _endVertex;

    private readonly AdjacencyList _adjList;

    public Dijkstra(AdjacencyList adjList, int startVertex, int endVertex)
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

        Dictionary<int, bool> unvisited = new();

        // Init visited and unvisited vertices
        int totalVertices = _adjList.NoOfVertices;

        // Init tracking table
        for (var i = 0; i < totalVertices; i++)
        {
            if (_startVertex != i)
            {
                distances.Add(i, decimal.MaxValue);
            }
            unvisited.Add(i, true);
        }

        int totalVisitedVertices = 0;
        while (totalVisitedVertices != totalVertices)
        {
            List<int> unvisitedVetices = unvisited.Keys.ToList();
            int minVertice = unvisitedVetices[0];
            foreach (var vertice in unvisitedVetices)
            {
                if (distances[vertice] < distances[minVertice])
                {
                    minVertice = vertice;
                }
            }

            var adjEdges = _adjList.Vertices[minVertice];
            foreach (var adjEdge in adjEdges)
            {
                decimal newDistance = distances[minVertice] + adjEdge.Weight;
                if (newDistance < distances[adjEdge.Vertex])
                {
                    distances[adjEdge.Vertex] = distances[minVertice] + adjEdge.Weight;
                    prevNodes[adjEdge.Vertex] = minVertice;
                }
            }
            unvisited.Remove(minVertice);
            ++totalVisitedVertices;
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
}

