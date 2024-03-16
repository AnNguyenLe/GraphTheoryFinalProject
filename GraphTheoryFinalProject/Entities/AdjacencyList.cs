namespace GraphTheoryFinalProject.Entities;

public interface IAdjacencyList
{
    Dictionary<int, List<AdjacentEdge>> ConvertTextToAdjacencyList(string filePath);
}

public class AdjacentEdge
{
    public int Vertex { get; init; }
    public decimal Weight { get; init; }

    public AdjacentEdge(int vertex, decimal weight)
    {
        Vertex = vertex;
        Weight = weight;
    }
}

public class AdjacencyList : IAdjacencyList
{
    public int NoOfVertices { get; init; }
    public Dictionary<int, List<AdjacentEdge>> Vertices { get; init; }

    public AdjacencyList(string filePath)
    {
        var adjList = ConvertTextToAdjacencyList(filePath);
        NoOfVertices = adjList.Keys.Count;
        Vertices = adjList;
    }

    public AdjacencyList(int noOfVertices, Dictionary<int, List<AdjacentEdge>> adjListData)
    {
        NoOfVertices = noOfVertices;
        Vertices = adjListData;
    }

    public Dictionary<int, List<AdjacentEdge>> ConvertTextToAdjacencyList(string filePath)
    {
        Dictionary<int, List<AdjacentEdge>> graph = new();

        try
        {
            using var sr = new StreamReader(filePath);

            var CanNoOfVerticesBeParsed = int.TryParse(sr.ReadLine(), out int noOfVertices);
            if (!CanNoOfVerticesBeParsed)
            {
                throw new IOException("Read number of vertices failed!");
            }


            for (var i = 0; i < noOfVertices; i++)
            {
                var dataInText = sr.ReadLine();
                if (string.IsNullOrEmpty(dataInText))
                {
                    throw new IOException("Cannot parse null or empty string");
                }
                graph[i] = ConvertToListOfAdjacentEdge(dataInText);
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return graph;
    }

    public static AdjacencyList Clone(AdjacencyList adjList)
    {
        var clonedAdjListData = new Dictionary<int, List<AdjacentEdge>>();

        foreach (var vertex in adjList.Vertices.Keys)
        {
            var clonedAdjEdges = new List<AdjacentEdge>();
            foreach (var adjEdge in adjList.Vertices[vertex])
            {
                var clonedAdjEdge = new AdjacentEdge(adjEdge.Vertex, adjEdge.Weight);
                clonedAdjEdges.Add(clonedAdjEdge);
            }
            clonedAdjListData.Add(vertex, clonedAdjEdges);
        }

        return new AdjacencyList(adjList.NoOfVertices, clonedAdjListData);
    }

    public int CountTotalEdges()
    {
        return Vertices.Values.Aggregate(0, (acc, listOfAdjEdges) => acc + listOfAdjEdges.Count) / 2;
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
}

