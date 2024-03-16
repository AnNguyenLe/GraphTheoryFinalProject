using System.Collections;
using GraphTheoryFinalProject.Entities;

namespace GraphTheoryFinalProject.DirectedGraphs.StronglyConnectedComponents;

public class Tarjan
{
    private readonly AdjacencyList _adjList;
    public Tarjan(AdjacencyList adjList)
    {
        _adjList = adjList;
    }

    public IEnumerable<IEnumerable<int>> FindSCCs()
    {
        var totalVertices = _adjList.NoOfVertices;

        var lowLinkValues = InitLowLinkValues(_adjList);

        var visited = new BitArray(totalVertices);

        var inStack = new BitArray(totalVertices);

        var stack = new Stack<int>();

        foreach (var vertex in _adjList.Vertices.Keys)
        {
            if (visited[vertex])
            {
                continue;
            }

            DFSInSCC(vertex, stack, inStack, visited, lowLinkValues);
        }

        return lowLinkValues
            .Select((v, idx) => new { Value = v, Index = idx })
            .GroupBy(g => g.Value)
            .Select(g => g.Select(g => g.Index));
    }

    private void DFSInSCC(int atVertex, Stack<int> stack, BitArray inStack, BitArray visited, List<int> lowLinkValues)
    {
        stack.Push(atVertex);
        inStack.Set(atVertex, true);
        visited.Set(atVertex, true);

        foreach (var adjEdge in _adjList.Vertices[atVertex])
        {
            var nextVertex = adjEdge.Vertex;
            if (!visited[nextVertex])
            {
                DFSInSCC(nextVertex, stack, inStack, visited, lowLinkValues);
            }

            if (inStack[nextVertex])
            {
                lowLinkValues[atVertex] = int.Min(lowLinkValues[nextVertex], lowLinkValues[atVertex]);
            }
        }

        if (lowLinkValues[atVertex] == atVertex)
        {
            var vertex = stack.Pop();
            while (vertex != atVertex)
            {
                inStack[vertex] = false;
                vertex = stack.Pop();
            }
        }
    }

    public static void Display(IEnumerable<IEnumerable<int>> sccs)
    {
        var totalGroups = sccs.Count();
        for (int i = 0; i < totalGroups; i++)
        {
            Console.WriteLine($"Strongly connected component {i + 1}: {string.Join(", ", sccs.ElementAt(i))}");
        }
    }

    private static List<int> InitLowLinkValues(AdjacencyList adjList)
    {
        var values = new List<int>();
        for (int i = 0; i < adjList.NoOfVertices; i++)
        {
            values.Add(i);
        }
        return values;
    }
}

