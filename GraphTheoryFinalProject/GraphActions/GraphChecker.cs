using GraphTheoryFinalProject.Entities;
using System.Collections;

namespace GraphTheoryFinalProject.GraphActions;

public interface IGraphChecker
{
    bool IsConnectedGraph(int startVertex = 0);
    bool IsPositiveWeightedGraph();
    //bool IsBridge(Edge edge);
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
        var visitedVertices = new BitArray(_adjList.NoOfVertices);
        var orderOfVisitedVertices = new List<int>();

        (new GraphTraversal(_adjList)).DFS(startVertex, visitedVertices, orderOfVisitedVertices);

        return visitedVertices.Cast<bool>().All(value => value == true);
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

    //public bool IsBridge(Edge edge)
    //{
    //    var clonedAdjListData = _adjList.Vertices.ToDictionary(entry => entry.Key, entry => entry.Value.ToList());
    //    var adjEdgeStartVertexIndex = _adjList.Vertices[edge.StartVertex].FindIndex(endVertex => endVertex.Vertex == edge.EndVertex);
    //    var adjEdgeEndVertexIndex = _adjList.Vertices[edge.EndVertex].FindIndex(endVertex => endVertex.Vertex == edge.StartVertex);

    //    clonedAdjListData[edge.StartVertex].RemoveAt(adjEdgeStartVertexIndex);
    //    clonedAdjListData[edge.EndVertex].RemoveAt(adjEdgeEndVertexIndex);

    //    return IsConnectedGraph(new AdjacencyList(_adjList.NoOfVertices, clonedAdjListData));
    //}
}

