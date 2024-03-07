using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.GraphActions;
using GraphTheoryFinalProject.ShortesPathAlgos;

namespace GraphTheoryFinalProject.SolutionForProblems;

public class SolutionForProblem4
{
    public static void Run(string filePath)
    {
        Console.WriteLine("\nYêu cầu 4: Tìm đường đi ngắn nhất sử dụng giải thuật Floyd-Warshall:\n");
        var adjList = new AdjacencyList(filePath);
        IGraphChecker graphChecker = new GraphChecker(adjList);
        if (!graphChecker.IsPositiveWeightedGraph())
        {
            Console.WriteLine("Đồ thị không phải là Đồ thị có trọng số dương!");
            return;
        }

        var shortestPaths = new FloydWarshall(new AdjacencyMatrix(adjList));
        var data = shortestPaths.Generate();
        shortestPaths.Display(data);
    }
}

