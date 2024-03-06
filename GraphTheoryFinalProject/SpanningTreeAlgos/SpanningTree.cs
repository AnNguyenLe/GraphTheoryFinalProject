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
            Console.WriteLine("Đồ thị này không liên thông => Không có cây khung.");
            return;
        }

        Console.WriteLine($"\nGiải thuật {algoType}");

        Console.WriteLine("\nTập cạnh của cây khung");

        decimal totalWeight = 0;

        foreach (var edge in spanningTree)
        {
            totalWeight += edge.Weight;
            Console.WriteLine($"{edge.StartVertex} - {edge.EndVertex}: {edge.Weight}");
        }

        Console.WriteLine($"\nTrọng số của cây khung: {totalWeight}\n");
    }
}

