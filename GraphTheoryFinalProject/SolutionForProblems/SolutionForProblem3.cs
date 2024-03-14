using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.Enums;
using GraphTheoryFinalProject.SpanningTreeAlgos;

namespace GraphTheoryFinalProject.SolutionForProblems;

public class SolutionForProblem3
{
    public static void Run(string filePath)
    {
        // ============== Yeu cau 3: ===============
        Console.WriteLine("\nProblem 3: Find the maximum spanning tree using Prim and Kruskal algorithms\n");

        var adjList = new AdjacencyList(filePath);

        var adjMatrix = new AdjacencyMatrix(adjList);

        IPrimSpanningTree primMaxSpanningTree = new PrimSpanningTree(adjList, TypesOfSpanningTree.MAXIMUM);
        primMaxSpanningTree.Display(SpanningTreeAlgorithms.PRIM, primMaxSpanningTree.Generate(0));

        IKruskalSpanningTree kruskalMaxSpanningTree = new KruskalSpanningTree(adjList, TypesOfSpanningTree.MAXIMUM);
        kruskalMaxSpanningTree.Display(SpanningTreeAlgorithms.KRUSKAL, kruskalMaxSpanningTree.Generate());
    }
}

