using GraphTheoryFinalProject.Entities;
using System.Collections;

namespace GraphTheoryFinalProject.GraphActions;

public interface IGraphTraversal
{
    void DFS(int startVertex, BitArray visited, List<int> orderOfVisited);
}

public class GraphTraversal : IGraphTraversal
{
    private readonly AdjacencyList _adjList;

    public GraphTraversal(AdjacencyList adjList)
    {
        _adjList = adjList;
    }

    public void DFS(int startVertex, BitArray visited, List<int> orderOfVisited)
    {
        if (visited[startVertex])
        {
            return;
        }
        foreach (var edge in _adjList.Vertices[startVertex])
        {

            if (!visited[startVertex])
            {
                orderOfVisited.Add(startVertex);
                visited.Set(startVertex, true);
            }

            DFS(edge.Vertex, visited, orderOfVisited);
        }
    }
}



