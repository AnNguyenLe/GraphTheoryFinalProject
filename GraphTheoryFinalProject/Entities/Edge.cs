namespace GraphTheoryFinalProject.Entities;

public class Edge
{
    public int StartVertex { get; init; }
    public int EndVertex { get; init; }
    public decimal Weight { get; init; }
    public Edge(int startVertex, int endVertex, decimal weight)
    {
        StartVertex = startVertex;
        EndVertex = endVertex;
        Weight = weight;
    }
}

