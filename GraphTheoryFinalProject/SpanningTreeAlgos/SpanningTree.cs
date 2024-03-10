using GraphTheoryFinalProject.Entities;
using GraphTheoryFinalProject.Enums;

namespace GraphTheoryFinalProject.SpanningTreeAlgos;

public interface ISpanningTree
{
    void Display(SpanningTreeAlgorithms algoType, List<Edge> spanningTree);
}

public class SpanningTree : ISpanningTree
{
    public void Display(SpanningTreeAlgorithms algoType, List<Edge> spanningTree)
    {
        if (spanningTree[0].StartVertex == -1)
        {
            Console.WriteLine("This is NOT a connected graph. Finding maximum spanning tree program stopped.\n");
            return;
        }

        Console.WriteLine($"\n{algoType} Algorithm");

        Console.WriteLine("\nEdge set of the spanning tree");

        decimal totalWeight = 0;

        foreach (var edge in spanningTree)
        {
            totalWeight += edge.Weight;
            Console.WriteLine($"{edge.StartVertex} - {edge.EndVertex}: {edge.Weight}");
        }

        Console.WriteLine($"\nTotal weight of the spanning tree: {totalWeight}\n");
    }
}

