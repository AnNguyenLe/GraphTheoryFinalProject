namespace GraphTheoryFinalProject.Entities;

public interface IAdjacencyMatrix
{
    decimal[,] ConvertTextToAdjacencyMatrix(string filePath);
}

public class AdjacencyMatrix : IAdjacencyMatrix
{
    public int NoOfVertices { get; init; }
    public decimal[,] Data { get; init; }

    public AdjacencyMatrix(string filePath)
    {
        var graph = ConvertTextToAdjacencyMatrix(filePath);
        NoOfVertices = graph.GetLength(0);
        Data = graph;
    }

    public AdjacencyMatrix(AdjacencyList adjacencyList)
    {
        NoOfVertices = adjacencyList.NoOfVertices;
        Data = ConvertToAdjacencyMatrix(adjacencyList);
    }

    public decimal[,] ConvertTextToAdjacencyMatrix(string filePath)
    {
        decimal[,] graph = null;
        try
        {
            using var sr = new StreamReader(filePath);
            int noOfVertices = int.Parse(sr.ReadLine());
            graph = new decimal[noOfVertices, noOfVertices];

            for (var i = 0; i < noOfVertices; i++)
            {
                var adjEdges = ConvertToListOfAdjacentEdge(sr.ReadLine());
                foreach (var edge in adjEdges)
                {
                    graph[i, edge.Vertex] = edge.Weight;
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        return graph;
    }

    private static List<AdjacentEdge> ConvertToListOfAdjacentEdge(string dataInText)
    {
        string[] adjInfo = dataInText.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var list = new List<AdjacentEdge>();

        int noOfAdjacentEdges = int.Parse(adjInfo[0]);
        for (int i = 0; i < noOfAdjacentEdges; i++)
        {
            int adjVertex = int.Parse(adjInfo[2 * i + 1]);
            decimal weight = decimal.Parse(adjInfo[2 * i + 2]);

            AdjacentEdge data = new(adjVertex, weight);
            list.Add(data);
        }
        return list;
    }

    private static decimal[,] ConvertToAdjacencyMatrix(AdjacencyList adjList)
    {
        var adjMatrix = new decimal[adjList.NoOfVertices, adjList.NoOfVertices];

        foreach (var vertex in adjList.Vertices.Keys)
        {
            var noOfAdjEdges = adjList.Vertices[vertex].Count;
            for (int i = 0; i < noOfAdjEdges; i++)
            {
                var adjEdge = adjList.Vertices[vertex][i];
                if (adjMatrix[vertex, adjEdge.Vertex] == 0 && adjMatrix[vertex, adjEdge.Vertex] == adjMatrix[adjEdge.Vertex, vertex])
                {
                    adjMatrix[vertex, adjEdge.Vertex] = adjMatrix[adjEdge.Vertex, vertex] = adjEdge.Weight;
                }
            }
        }

        return adjMatrix;
    }
}

