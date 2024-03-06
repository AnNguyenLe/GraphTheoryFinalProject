﻿namespace GraphTheoryFinalProject.Entities;

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

    public Dictionary<int, List<AdjacentEdge>> ConvertTextToAdjacencyList(string filePath)
    {
        Dictionary<int, List<AdjacentEdge>> graph = new();

        try
        {
            using var sr = new StreamReader(filePath);
            int noOfVertices = int.Parse(sr.ReadLine());

            for (var i = 0; i < noOfVertices; i++)
            {
                graph[i] = ConvertToListOfAdjacentEdge(sr.ReadLine()); ;
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
}
