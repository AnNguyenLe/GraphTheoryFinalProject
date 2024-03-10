using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.GraphActions;
using GraphTheoryFinalProject.EulerPathAlgos;

namespace GraphTheoryFinalProject.SolutionForProblems;

public class SolutionForProblem5
{
    public static void Run(string filePath)
    {
        // ============== Yeu cau 5: ===============
        Console.WriteLine("\nProblem 5: Identify Eulerian Circuit or Path\n");

        var adjList = new AdjacencyList(filePath);

        IGraphChecker graphChecker = new GraphChecker(adjList);
        if (!graphChecker.IsConnectedGraph())
        {
            Console.WriteLine("This is NOT a connected graph => Euler path NOT exists.\n");
            return;
        }

        var oddDegreeVertices = GetOddDegreeVertices(adjList);

        if (oddDegreeVertices.Count > 2)
        {
            Console.WriteLine("Euler path does NOT exist in this graph.");
            return;
        }

        // Generate the list of vertex on path
        var fleurySolution = new Fleury(adjList, oddDegreeVertices);
        var path = fleurySolution.Generate();

        // Display to the console
        fleurySolution.Display(path);
    }

    private static List<int> GetOddDegreeVertices(AdjacencyList adjList)
    {
        var oddDegreeVertices = new List<int>();

        foreach (var vertex in adjList.Vertices.Keys)
        {
            if (adjList.Vertices[vertex].Count % 2 != 0)
            {
                oddDegreeVertices.Add(vertex);
            }
        }

        return oddDegreeVertices;
    }

}

