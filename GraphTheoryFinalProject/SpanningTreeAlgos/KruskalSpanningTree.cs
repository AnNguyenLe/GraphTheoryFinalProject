using GraphTheoryFinalProject.Comparers;
using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.Enums;
using GraphTheoryFinalProject.Verifiers;

namespace GraphTheoryFinalProject.SpanningTreeAlgos;

public interface IKruskalSpanningTree : ISpanningTree
{
    List<Edge> Generate();
}

public class KruskalSpanningTree : SpanningTree, IKruskalSpanningTree
{
    private readonly AdjacencyList _adjList;
    private readonly TypesOfSpanningTree _type;

    public KruskalSpanningTree(AdjacencyList adjList, TypesOfSpanningTree type)
    {
        _adjList = adjList;
        _type = type;
    }

    public List<Edge> Generate()
    {
        IGraphChecker graph = new GraphChecker(_adjList);
        if (!graph.IsConnectedGraph())
        {
            return new List<Edge>(1) { new Edge(-1, -1, -1) };
        }

        var disjointSetOfVertices = Enumerable.Repeat(-1, _adjList.NoOfVertices).ToArray();

        var heapOfEdges = SortedEdges();

        var spanningTree = new List<Edge>(_adjList.NoOfVertices - 1);

        int counter = 1;
        while (counter < _adjList.NoOfVertices)
        {
            var edge = heapOfEdges.Dequeue();
            if (!IsFormingACycle(edge, disjointSetOfVertices))
            {
                spanningTree.Add(edge);
                UpdateDisjointSet(edge, disjointSetOfVertices);
                ++counter;
            }
        }

        return spanningTree;
    }

    private PriorityQueue<Edge, decimal> SortedEdges()
    {
        var heap = new PriorityQueue<Edge, decimal>(new EdgeWeightComparer(_type));
        var adjMatrix = new AdjacencyMatrix(_adjList);
        for (int i = 0; i < adjMatrix.NoOfVertices; i++)
        {
            for (int j = i + 1; j < adjMatrix.NoOfVertices; j++)
            {
                var weight = adjMatrix.Data[i, j];
                if (weight != 0)
                {
                    heap.Enqueue(new Edge(i, j, weight), weight);
                }
            }
        }
        return heap;
    }

    private void UpdateDisjointSet(Edge edge, int[] disjointSet)
    {
        if (disjointSet[edge.StartVertex] < disjointSet[edge.EndVertex])
        {
            disjointSet[edge.StartVertex] += disjointSet[edge.EndVertex];
            disjointSet[edge.EndVertex] = edge.StartVertex;
        }
        else
        {
            disjointSet[edge.EndVertex] += disjointSet[edge.StartVertex];
            disjointSet[edge.StartVertex] = edge.EndVertex;
        }
    }

    private int GetRootVertex(int vertex, int[] disjointSet)
    {
        var parentVertex = disjointSet[vertex];
        while (parentVertex > 0)
        {
            parentVertex = disjointSet[parentVertex];
        }
        return parentVertex;
    }

    private bool IsFormingACycle(Edge edge, int[] disjointSet)
    {
        int rootOfStartVertex = -1, rootOfEndVertex = -1;
        var endpoints = new int[2] { edge.StartVertex, edge.EndVertex };
        for (int i = 0; i < endpoints.Length; i++)
        {
            int vertex = endpoints[i];
            var rootVertex = GetRootVertex(vertex, disjointSet);

            if (rootVertex == -1)
            {
                return false;
            }

            if (i == 0)
            {
                rootOfStartVertex = rootVertex;
            }
            else
            {
                rootOfEndVertex = rootVertex;
            }
        }

        return rootOfStartVertex == rootOfEndVertex;
    }


}

