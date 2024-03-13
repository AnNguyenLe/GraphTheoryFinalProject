using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.GraphActions;
using GraphTheoryFinalProject.SpecialGraphs;

namespace GraphTheoryFinalProject.SolutionForProblems;

public class SolutionForProblem1
{
    public static void Run(string filePath)
    {
        // ============== Yeu cau 1: ===============
        Console.WriteLine("\nProblem 1: Identify special graphs - Windmill, Barbell and k-partite graphs\n");

        var adjList = new AdjacencyList(filePath);

        var graphChecker = new GraphChecker(adjList);

        if (!graphChecker.IsConnectedGraph())
        {
            Console.WriteLine("Program stopped. This is NOT a connected.");
            return;
        }

        if (graphChecker.AnyLoopsOrMultiples())
        {
            Console.WriteLine("Program stopped. This is NOT a simple graph. Either contains multiples or loops.");
            return;
        }

        var windmillGraph = new Windmill_KComplete(adjList, 3);
        windmillGraph.Display();

        var barbellGraph = new Barbell(adjList);
        barbellGraph.Display();

        var kPartiteGraph = new KPartite(adjList, 3);
        kPartiteGraph.Display(kPartiteGraph.IdentifyGroups());
    }
}

