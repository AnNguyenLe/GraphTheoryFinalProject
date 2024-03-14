using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.Verifiers;
using GraphTheoryFinalProject.ShortesPathAlgos;

namespace GraphTheoryFinalProject.SolutionForProblems;

public class SolutionForProblem4
{
    public static void Run(string filePath)
    {
        Console.WriteLine("\nProblem 4: Find the shortest path using Floyd-Warshall algorithm\n");
        var adjList = new AdjacencyList(filePath);
        IGraphChecker graphChecker = new GraphChecker(adjList);
        if (!graphChecker.IsPositiveWeightedGraph())
        {
            Console.WriteLine("This is NOT positive weighted graph!");
            return;
        }

        var shortestPaths = new FloydWarshall(new AdjacencyMatrix(adjList));
        var data = shortestPaths.Generate();
        shortestPaths.Display(data);
    }
}

