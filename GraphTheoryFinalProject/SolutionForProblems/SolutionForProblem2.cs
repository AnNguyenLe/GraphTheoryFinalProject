using GraphTheoryFinalProject.DirectedGraphs;
using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.Verifiers;
using GraphTheoryFinalProject.Enums;
using GraphTheoryFinalProject.DirectedGraphs.StronglyConnectedComponents;

namespace GraphTheoryFinalProject.SolutionForProblems;

public class SolutionForProblem2
{
    public static void Run(string filePath)
    {
        // ============== Yeu cau 2: ===============
        Console.WriteLine("\nProblem 2: Identify strongly connected component(s)\n");

        var adjList = new AdjacencyList(filePath);

        var graphChecker = new GraphChecker(adjList);

        if (graphChecker.AnyLoopsOrMultiples())
        {
            Console.WriteLine("Program stopped. This is NOT a simple directed graph. Either contains multiples or loops.");
            return;
        }

        var adjMatrix = new AdjacencyMatrix(adjList);

        var connectityStatus = (new Connectivity(adjMatrix)).CheckConnectivityStatus();

        var graphConnectivityType = connectityStatus switch
        {
            ConnectivityStatus.StronglyConnected => "Strongly Connected Graph.",
            ConnectivityStatus.UnilaterallyConnected => "Unilaterally Connected Graph.",
            ConnectivityStatus.WeaklyConnected => "Weakly Connected Graph.",
            ConnectivityStatus.Disconnected => "Disconnected Graph.",
            _ => "Unidentified Graph",
        };

        Console.WriteLine($"This is {graphConnectivityType}");

        var tarjanSCCs = new Tarjan(adjList);
        Tarjan.Display(tarjanSCCs.FindSCCs());
    }
}

