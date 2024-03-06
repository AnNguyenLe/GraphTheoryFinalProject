using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.Enums;
using GraphTheoryFinalProject.SpanningTreeAlgos;

namespace GraphTheoryFinalProject;

class Program
{
    static void Main(string[] args)
    {
        // ============== Yeu cau 3: ===============
        const string FILEPATH_3 = "/Users/thienannguyenle/Downloads/GraphTheoryFinalProject_Samples/connected_undirected_graph_3.txt";
        var adjList = new AdjacencyList(FILEPATH_3);

        IPrimSpanningTree primMaxSpanningTree = new PrimSpanningTree(adjList, TypesOfSpanningTree.MAXIMUM);
        primMaxSpanningTree.Display(SpanningTreeAlgorithms.PRIM, primMaxSpanningTree.Generate(0));

        IKruskalSpanningTree kruskalMaxSpanningTree = new KruskalSpanningTree(adjList, TypesOfSpanningTree.MAXIMUM);
        kruskalMaxSpanningTree.Display(SpanningTreeAlgorithms.KRUSKAL, kruskalMaxSpanningTree.Generate());
        Console.Read();
    }
}