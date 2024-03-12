using GraphTheoryFinalProject.Entities;

namespace GraphTheoryFinalProject.SpecialGraphs
{
    public class Windmill_KComplete
    {
        private readonly AdjacencyList _adjList;
        public readonly int _kComplete;
        public Windmill_KComplete(AdjacencyList adjList, int kComplete)
        {
            _adjList = adjList;
            _kComplete = kComplete;
        }


        public int CountKCompleteCopies()
        {
            // each copies constructed from list of vertices
            Dictionary<int, List<int>> copies = new();
            Dictionary<int, int> groupOfVertices = new();

            var centerVertex = GetCenterVertex();

            int currentId = 0;

            foreach (var adjEdge in _adjList.Vertices[centerVertex])
            {
                if (groupOfVertices.ContainsKey(adjEdge.Vertex))
                {
                    continue;
                }
                var vertices = new List<int>();
                foreach (var adjEdgeInSubGraph in _adjList.Vertices[adjEdge.Vertex])
                {
                    var vertex = adjEdgeInSubGraph.Vertex;
                    vertices.Add(vertex);
                    if (vertex != centerVertex)
                    {
                        groupOfVertices.Add(vertex, currentId);
                    }
                }

                copies.Add(currentId++, vertices);
            }

            var noOfKCompletes = copies.Keys.Count;

            var totalEdges = _adjList.CountTotalEdges();
            var validNoOfVertices = noOfKCompletes * (_kComplete - 1) + 1;
            var validNoOfEdges = noOfKCompletes * _kComplete * (_kComplete - 1) / 2;

            if (_adjList.NoOfVertices != validNoOfVertices || totalEdges != validNoOfEdges)
            {
                return -1;
            }

            return noOfKCompletes;
        }

        public void Display()
        {
            Console.Write("Windmill graph: ");
            var noOfKCompletes = CountKCompleteCopies();
            if (noOfKCompletes == -1)
            {
                Console.WriteLine("None.");
            }
            else
            {
                Console.WriteLine($"Wd({_kComplete},{noOfKCompletes})");
            }
        }

        private int GetCenterVertex()
        {
            return _adjList.Vertices.Keys.MaxBy(vertex => _adjList.Vertices[vertex].Count);
        }
    }


}

