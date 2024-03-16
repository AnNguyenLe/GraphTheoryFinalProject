using GraphTheoryFinalProject.Entities;

namespace GraphTheoryFinalProject.SpecialGraphs;

public class KPartite
{
    private readonly AdjacencyList _adjList;
    private readonly AdjacencyMatrix _adjMatrix;
    private readonly int _kPartite;
    public KPartite(AdjacencyList adjList, int kPartite)
    {
        _adjList = adjList;
        _adjMatrix = new AdjacencyMatrix(_adjList);
        _kPartite = kPartite;
    }

    public List<List<int>>? IdentifyGroups()
    {
        Dictionary<int, List<int>> groupsOfVertices = InitDisjointSets();

        foreach (var vertex in _adjList.Vertices.Keys)
        {
            foreach (var groupId in groupsOfVertices.Keys)
            {
                var setOfVertices = groupsOfVertices[groupId];
                // this vertex is NOT in the groupId and NOT adjacent to any of those vertices already in the group
                if (!setOfVertices.Contains(vertex) && CountDirectPaths(vertex, setOfVertices) == 0)
                {
                    setOfVertices.Add(vertex);
                    break;
                }
            }
        }

        var totalVerticesHadBeenAdded = groupsOfVertices.Values.Aggregate(0, (acc, list) => acc + list.Count);
        if (totalVerticesHadBeenAdded < _adjList.NoOfVertices)
        {
            return null;
        }
        List<List<int>> sets = new();
        foreach (var set in groupsOfVertices.Values)
        {
            sets.Add(set);
        }
        return sets;
    }

    public void Display(List<List<int>>? sets)
    {
        Console.Write($"k-partite graph: {_kPartite} partite ");

        if (sets == null)
        {
            Console.WriteLine("None.");
            return;
        }

        foreach (var set in sets)
        {
            string setInText = "{ ";
            setInText += string.Join(", ", set);
            setInText += " } ";

            Console.Write(setInText);
        }

        Console.WriteLine("\n\n");
    }

    private int CountDirectPaths(int fromVertex, List<int> toTheseVertices)
    {
        int counter = 0;
        foreach (var toVertex in toTheseVertices)
        {
            if (_adjMatrix.Data[fromVertex, toVertex] != 0)
            {
                ++counter;
            }
        }
        return counter;
    }

    private Dictionary<int, List<int>> InitDisjointSets()
    {
        Dictionary<int, List<int>> groupsOfVertices = new();
        for (int i = 0; i < _kPartite; i++)
        {
            groupsOfVertices[i] = new List<int>();
        }
        return groupsOfVertices;
    }
}

