using GraphTheoryFinalProject.Enums;

namespace GraphTheoryFinalProject.Comparers;

public class EdgeWeightComparer : IComparer<decimal>
{
    private readonly TypesOfSpanningTree _type;

    public EdgeWeightComparer(TypesOfSpanningTree type)
    {
        _type = type;
    }

    public int Compare(decimal a, decimal b)
    {
        return (int)(_type == TypesOfSpanningTree.MINIMUM ? a - b : b - a);
    }
}

