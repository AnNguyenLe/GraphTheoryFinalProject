using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.Enums;
using GraphTheoryFinalProject.SpanningTreeAlgos;

namespace GraphTheoryFinalProject.SolutionForProblems;

public class SolutionForProblem3
{
    public static void Run(string filePath)
    {
        // ============== Yeu cau 3: ===============
        var adjList = new AdjacencyList(filePath);

        IPrimSpanningTree primMaxSpanningTree = new PrimSpanningTree(adjList, TypesOfSpanningTree.MAXIMUM);
        primMaxSpanningTree.Display(SpanningTreeAlgorithms.PRIM, primMaxSpanningTree.Generate(0));

        IKruskalSpanningTree kruskalMaxSpanningTree = new KruskalSpanningTree(adjList, TypesOfSpanningTree.MAXIMUM);
        kruskalMaxSpanningTree.Display(SpanningTreeAlgorithms.KRUSKAL, kruskalMaxSpanningTree.Generate());
    }
}

