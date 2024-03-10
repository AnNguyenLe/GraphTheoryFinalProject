using GraphTheoryFinalProject.Entities;
namespace GraphTheoryFinalProject.ShortesPathAlgos;

public class FloydWarshallAPSP
{
    public int[,] PathMatrix;
    public decimal[,] MinCostMatrix;

    public FloydWarshallAPSP(int[,] pathMatrix, decimal[,] minCostMatrix)
    {
        PathMatrix = pathMatrix;
        MinCostMatrix = minCostMatrix;
    }
}

public class FloydWarshall
{
    private readonly AdjacencyMatrix _adjMatrix;
    public FloydWarshall(AdjacencyMatrix adjMatrix)
    {
        _adjMatrix = adjMatrix;
    }

    public FloydWarshallAPSP Generate()
    {
        int totalVertices = _adjMatrix.NoOfVertices;
        var minCostMatrix = InitMinCostMatrix(_adjMatrix);
        var pathMatrix = InitPathMatrix(_adjMatrix);

        for (int k = 0; k < totalVertices; k++)
        {
            for (int i = 0; i < totalVertices; i++)
            {
                for (int j = 0; j < totalVertices; j++)
                {
                    if (minCostMatrix[i, j] > minCostMatrix[i, k] + minCostMatrix[k, j])
                    {
                        minCostMatrix[i, j] = minCostMatrix[i, k] + minCostMatrix[k, j];
                        pathMatrix[i, j] = pathMatrix[k, j];
                    }
                }
            }
        }

        return new FloydWarshallAPSP(pathMatrix, minCostMatrix);
    }

    public void Display(FloydWarshallAPSP data)
    {
        for (int i = 0; i < _adjMatrix.NoOfVertices; i++)
        {
            Console.WriteLine($"\nShortest paths from vertex {i}");
            for (int j = 0; j < _adjMatrix.NoOfVertices; j++)
            {
                if (i == j) continue;
                var path = string.Join(" -> ", GeneratePath(i, j, data.PathMatrix));
                var cost = data.MinCostMatrix[i, j];
                Console.WriteLine($"{path} : {cost}");
            }
        }
    }

    private static List<int> GeneratePath(int startVertex, int endVertex, int[,] pathMatrix)
    {
        var path = new List<int>();
        if (pathMatrix[startVertex, endVertex] == -1)
        {
            return path;
        }

        path.Add(endVertex);
        while (startVertex != endVertex)
        {
            endVertex = pathMatrix[startVertex, endVertex];
            path.Add(endVertex);
        }

        path.Reverse();

        return path;
    }

    private static int[,] InitPathMatrix(AdjacencyMatrix adjMatrix)
    {
        var noOfVertices = adjMatrix.NoOfVertices;
        var matrix = new int[noOfVertices, noOfVertices];
        for (int i = 0; i < noOfVertices; i++)
        {
            for (int j = 0; j < noOfVertices; j++)
            {
                if (adjMatrix.Data[i, j] == int.MaxValue)
                {
                    matrix[i, j] = -1;
                }
                else
                {
                    matrix[i, j] = i;
                }
            }
        }
        return matrix;
    }

    private static decimal[,] InitMinCostMatrix(AdjacencyMatrix adjMatrix)
    {
        var matrix = new decimal[adjMatrix.NoOfVertices, adjMatrix.NoOfVertices];
        for (int i = 0; i < adjMatrix.Data.GetLength(0); i++)
        {
            for (int j = 0; j < adjMatrix.Data.GetLength(1); j++)
            {
                if (i == j)
                {
                    matrix[i, j] = 0;
                    continue;
                }
                if (adjMatrix.Data[i, j] == 0)
                {
                    matrix[i, j] = int.MaxValue;
                }
                else
                {
                    matrix[i, j] = adjMatrix.Data[i, j];

                }
            }
        }
        return matrix;
    }
}

