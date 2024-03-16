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

    public AdjacencyMatrix(int noOfVertices, decimal[,] data)
    {
        NoOfVertices = noOfVertices;
        Data = data;
    }

    public AdjacencyMatrix(AdjacencyList adjacencyList)
    {
        AdjacencyMatrix adjMatrix = TranslateFromAdjacencyList(adjacencyList);
        NoOfVertices = adjMatrix.NoOfVertices;
        Data = adjMatrix.Data;
    }

    public decimal[,] ConvertTextToAdjacencyMatrix(string filePath)
    {
        decimal[,] graph = new decimal[0, 0];
        try
        {
            using var sr = new StreamReader(filePath);

            var CanNoOfVerticesBeParsed = int.TryParse(sr.ReadLine(), out int noOfVertices);
            if (!CanNoOfVerticesBeParsed)
            {
                throw new IOException("Read number of vertices failed!");
            }

            graph = new decimal[noOfVertices, noOfVertices];

            for (var i = 0; i < noOfVertices; i++)
            {
                var dataInText = sr.ReadLine();
                if (string.IsNullOrEmpty(dataInText))
                {
                    throw new IOException("Cannot parse null or empty string");
                }

                var adjEdges = ConvertToListOfAdjacentEdge(dataInText);
                foreach (var edge in adjEdges)
                {
                    graph[i, edge.Vertex] = edge.Weight;
                }
            }
            return graph;
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read: ");
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

    public static AdjacencyMatrix TranslateFromAdjacencyList(AdjacencyList adjList)
    {
        var matrix = new decimal[adjList.NoOfVertices, adjList.NoOfVertices];

        var adjListData = adjList.Vertices;

        foreach (var fromVertex in adjListData.Keys)
        {
            foreach (var adjEdge in adjListData[fromVertex])
            {
                var toVertex = adjEdge.Vertex;
                var weight = adjEdge.Weight;
                matrix[fromVertex, toVertex] = weight;
            }
        }

        return new AdjacencyMatrix(adjList.NoOfVertices, matrix);
    }

    public static AdjacencyMatrix Transpose(AdjacencyMatrix matrix)
    {
        var originalMatrix = matrix.Data;
        var noOfVertices = matrix.NoOfVertices;

        var transposedMatrix = new decimal[noOfVertices, noOfVertices];

        for (int i = 0; i < noOfVertices; i++)
        {
            for (int j = 0; j < noOfVertices; j++)
            {
                transposedMatrix[j, i] = originalMatrix[i, j];
            }
        }

        return new AdjacencyMatrix(noOfVertices, transposedMatrix);
    }
}

