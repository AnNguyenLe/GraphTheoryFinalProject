using GraphTheoryFinalProject.Comparers;
using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.Enums;
using GraphTheoryFinalProject.GraphActions;
using System.Collections;

namespace GraphTheoryFinalProject.SpanningTreeAlgos;

public interface IPrimSpanningTree : ISpanningTree
{
    List<Edge> Generate(int startVertex = 0);
}
public class PrimSpanningTree : SpanningTree, IPrimSpanningTree
{
    private readonly AdjacencyList _adjList;
    private readonly TypesOfSpanningTree _type;

    public PrimSpanningTree(AdjacencyList adjList, TypesOfSpanningTree type)
    {
        _adjList = adjList;
        _type = type;
    }

    public List<Edge> Generate(int startVertex = 0)
    {
        IGraphChecker graph = new GraphChecker(_adjList);
        if (!graph.IsConnectedGraph())
        {
            return new List<Edge>(1) { new Edge(-1, -1, -1) };
        }
        var spanningTree = new List<Edge>(_adjList.NoOfVertices - 1);

        var visitedVertices = new BitArray(_adjList.NoOfVertices);

        var verticesOfSpanningTree = new List<int>()
        {
            startVertex
        };

        for (int counter = 1; counter < _adjList.NoOfVertices; counter++)
        {
            var heap = new PriorityQueue<Edge, decimal>(new EdgeWeightComparer(_type));

            foreach (var vertex in verticesOfSpanningTree)
            {
                foreach (var adjEdge in _adjList.Vertices[vertex])
                {
                    if (visitedVertices[vertex] && visitedVertices[adjEdge.Vertex])
                    {
                        continue;
                    }
                    heap.Enqueue(new Edge(vertex, adjEdge.Vertex, adjEdge.Weight), adjEdge.Weight);
                }
            }

            var rootOfHeap = heap.Dequeue();
            visitedVertices.Set(rootOfHeap.StartVertex, true);
            visitedVertices.Set(rootOfHeap.EndVertex, true);
            verticesOfSpanningTree.Add(rootOfHeap.EndVertex);
            spanningTree.Add(rootOfHeap);
        }

        return spanningTree;
    }
}
